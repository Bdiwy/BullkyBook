document.addEventListener('DOMContentLoaded', function () {
    var deleteModal = document.getElementById('deleteModal');
    if (deleteModal) {
        deleteModal.addEventListener('show.bs.modal', function (event) {
            var button = event.relatedTarget;
            var itemId = button.getAttribute('data-id');
            var itemName = button.getAttribute('data-name');
            var controllerName = button.getAttribute('data-controller') || 'Category'; // optional for dynamic controllers

            // Update modal content
            var modalTitle = deleteModal.querySelector('#categoryName');
            modalTitle.textContent = itemName;

            // Update form action dynamically
            var form = deleteModal.querySelector('#deleteForm');
            form.action = '/' + controllerName + '/Delete/' + itemId;
        });
    }
});
