document.addEventListener('DOMContentLoaded', function() {

    const deleteModal = document.getElementById('deleteModal');
    const deleteForm = document.getElementById('deleteForm');
    let currentCategoryId = null;

    if(!deleteModal || !deleteForm) return;

    // Show modal and fill data
    deleteModal.addEventListener('show.bs.modal', function(e) {
        const button = e.relatedTarget;
        currentCategoryId = button.getAttribute('data-id');
        const categoryName = button.getAttribute('data-name');

        document.getElementById('categoryName').textContent = categoryName;
        // Post to action without path id; pass id in body to match controller binding
        deleteForm.action = '/Category/Delete';
    });

    // Submit form via fetch
    deleteForm.addEventListener('submit', function(e) {
        e.preventDefault();

        const token = deleteForm.querySelector('input[name="__RequestVerificationToken"]').value;

        // Build form-encoded body with antiforgery token and id for robustness
        const formBody = new URLSearchParams();
        formBody.append('__RequestVerificationToken', token);
        if (currentCategoryId) {
            formBody.append('id', currentCategoryId);
        }

        fetch(deleteForm.action, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8',
                // Also pass token via header for compatibility
                'RequestVerificationToken': token
            },
            credentials: 'same-origin',
            body: formBody.toString()
        })
        .then(async res => {
            // Attempt to parse JSON; if not OK, throw with server text to show useful error
            const contentType = res.headers.get('Content-Type') || '';
            const isJson = contentType.includes('application/json');
            if (!res.ok) {
                const errText = await res.text().catch(() => '');
                throw new Error(errText || ('HTTP ' + res.status));
            }
            return isJson ? res.json() : { success: false, message: 'Unexpected response' };
        })
        .then(data => {
            console.log(data);
            
            if(data.success) {
                console.log("sdsddsdsfs");
                

                // Close modal
                const modalInstance = bootstrap.Modal.getInstance(deleteModal);
                modalInstance.hide();

                // Remove table row
                const row = document.querySelector('button[data-id="'+currentCategoryId+'"]').closest('tr');
                if(row) row.remove();

                // Show success alert
                const alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-dark alert-dismissible fade show';
                alertDiv.role = 'alert';
                alertDiv.innerHTML = `
                    ${data.message}
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                `;
                document.querySelector('.alertMessage').prepend(alertDiv);

                // Auto-dismiss success alert after 2 seconds
                setTimeout(() => {
                    try {
                        const bsAlert = bootstrap.Alert.getOrCreateInstance(alertDiv);
                        bsAlert.close();
                    } catch(_) {
                        // Fallback: remove element
                        alertDiv.remove();
                    }
                }, 2000);

            } else {
                alert(data.message || 'Delete failed!');
            }
        })
        .catch(err => {
            console.error('Delete error:', err);
            const message = (err && err.message) ? err.message : 'Something went wrong!';
            alert(message);
        });
    });

});
