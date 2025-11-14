// Import Categories Modal
const importModal = document.getElementById('importModal');
const confirmImportBtn = document.getElementById('confirmImportBtn');
const importStatus = document.getElementById('importStatus');

confirmImportBtn?.addEventListener("click", function () {
    const btn = this;
    const originalText = btn.innerHTML;

    // Disable button and show loading
    btn.disabled = true;
    btn.innerHTML = '<i class="bi bi-hourglass-split"></i> Importing...';
    importStatus.style.display = 'none';

    fetch('/api/fetch', {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                importStatus.innerHTML = '<div class="alert alert-success">Every Thing Is Okay!</div>';
                importStatus.style.display = 'block';

                // Close modal and reload after 1.5 seconds
                setTimeout(() => {
                    const modalInstance = bootstrap.Modal.getInstance(importModal);
                    modalInstance.hide();
                    window.location.reload();
                }, 1500);
            } else {
                importStatus.innerHTML = '<div class="alert alert-danger">' + (data.message || 'Faild Importing') + '</div>';
                importStatus.style.display = 'block';
                btn.disabled = false;
                btn.innerHTML = originalText;
            }
        })
        .catch(error => {
            console.error('Error:', error);
            importStatus.innerHTML = '<div class="alert alert-danger">Something Wrong Happen !!!: ' + error.message + '</div>';
            importStatus.style.display = 'block';
            btn.disabled = false;
            btn.innerHTML = originalText;
        });
});

// Reset modal when it's closed
importModal?.addEventListener('hidden.bs.modal', function () {
    importStatus.style.display = 'none';
    importStatus.innerHTML = '';
    confirmImportBtn.disabled = false;
    confirmImportBtn.innerHTML = '<i class="bi bi-download"></i> Import';
});