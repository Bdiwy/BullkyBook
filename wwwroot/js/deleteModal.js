document.addEventListener('DOMContentLoaded', function() {

    const deleteModal = document.getElementById('deleteModal');
    const deleteForm = document.getElementById('deleteForm');
    let currentId = null;

    if(!deleteModal || !deleteForm) return;

    const pathParts = window.location.pathname.split('/').filter(p => p.length > 0);
    const controllerName = pathParts[0] || '';

    deleteModal.addEventListener('show.bs.modal', function(e) {
        const button = e.relatedTarget;
        currentId = button.getAttribute('data-id');
        const currentName = button.getAttribute('data-name');

        document.getElementById('nameOFItem').textContent = currentName;
        deleteForm.action = '/' + controllerName + '/Delete';
    });

    deleteForm.addEventListener('submit', function(e) {
        e.preventDefault();

        const token = deleteForm.querySelector('input[name="__RequestVerificationToken"]').value;

        const formBody = new URLSearchParams();
        formBody.append('__RequestVerificationToken', token);
        if (currentId) formBody.append('id', currentId);

        fetch(deleteForm.action, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8',
                'RequestVerificationToken': token
            },
            credentials: 'same-origin',
            body: formBody.toString()
        })
        .then(async res => {
            const contentType = res.headers.get('Content-Type') || '';
            const isJson = contentType.includes('application/json');
            if (!res.ok) {
                const errText = await res.text().catch(() => '');
                throw new Error(errText || ('HTTP ' + res.status));
            }
            return isJson ? res.json() : { success: false, message: 'Unexpected response' };
        })
        .then(data => {
            if(data.success) {
                const modalInstance = bootstrap.Modal.getInstance(deleteModal);
                modalInstance.hide();

                const row = document.querySelector('button[data-id="'+currentId+'"]').closest('tr');
                if(row) row.remove();

                const alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-dark alert-dismissible fade show';
                alertDiv.role = 'alert';
                alertDiv.innerHTML = `
                    ${data.message}
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                `;
                document.querySelector('.alertMessage').prepend(alertDiv);

                setTimeout(() => {
                    try { bootstrap.Alert.getOrCreateInstance(alertDiv).close(); } catch(_) { alertDiv.remove(); }
                }, 2000);

            } else {
                alert(data.message || 'Delete failed!');
            }
        })
        .catch(err => {
            console.error('Delete error:', err);
            alert(err?.message || 'Something went wrong!');
        });
    });

});
