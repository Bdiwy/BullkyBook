// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Set active navbar link based on current page
document.addEventListener('DOMContentLoaded', function() {
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link');
    
    navLinks.forEach(link => {
        const linkPath = new URL(link.href).pathname.toLowerCase();
        
        // Remove active class from all links
        link.classList.remove('active');
        
        // Add active class if current path matches
        if (currentPath === linkPath || 
            (currentPath.startsWith('/category') && linkPath.includes('/category')) ||
            (currentPath === '/' && linkPath === '/')) {
            link.classList.add('active');
        }
    });
});