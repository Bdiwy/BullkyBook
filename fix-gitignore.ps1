# PowerShell script to fix .NET .gitignore issues
# Run this script if build artifacts (bin/, obj/, etc.) are being tracked by Git

Write-Host "=== Fixing .NET .gitignore ===" -ForegroundColor Cyan
Write-Host ""

# Step 1: Generate/Update .gitignore with official .NET template
Write-Host "Step 1: Generating official .NET .gitignore..." -ForegroundColor Yellow
dotnet new gitignore --force
if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ .gitignore updated" -ForegroundColor Green
} else {
    Write-Host "✗ Failed to generate .gitignore" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Step 2: Ensure wwwroot/lib/ is in .gitignore
Write-Host "Step 2: Checking wwwroot/lib/ in .gitignore..." -ForegroundColor Yellow
$gitignoreContent = Get-Content .gitignore -Raw
if ($gitignoreContent -notmatch "wwwroot/lib/") {
    Add-Content .gitignore "`n# Client-side libraries (usually restored via LibMan)`nwwwroot/lib/"
    Write-Host "✓ Added wwwroot/lib/ to .gitignore" -ForegroundColor Green
} else {
    Write-Host "✓ wwwroot/lib/ already in .gitignore" -ForegroundColor Green
}

Write-Host ""

# Step 3: Remove already-tracked files that should be ignored
Write-Host "Step 3: Removing build artifacts from Git tracking..." -ForegroundColor Yellow
Write-Host "  (Files will remain on disk, just removed from Git)" -ForegroundColor Gray

# Remove bin/ and obj/ directories
if (Test-Path "bin") {
    git rm -r --cached bin/ 2>$null
    Write-Host "✓ Removed bin/ from tracking" -ForegroundColor Green
}

if (Test-Path "obj") {
    git rm -r --cached obj/ 2>$null
    Write-Host "✓ Removed obj/ from tracking" -ForegroundColor Green
}

# Remove wwwroot/lib/ if it exists
if (Test-Path "wwwroot/lib") {
    git rm -r --cached wwwroot/lib/ 2>$null
    Write-Host "✓ Removed wwwroot/lib/ from tracking" -ForegroundColor Green
}

Write-Host ""

# Step 4: Stage the .gitignore changes
Write-Host "Step 4: Staging .gitignore changes..." -ForegroundColor Yellow
git add .gitignore
Write-Host "✓ .gitignore staged" -ForegroundColor Green

Write-Host ""
Write-Host "=== Done! ===" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "  1. Review changes: git status" -ForegroundColor White
Write-Host "  2. Commit: git commit -m 'Fix .gitignore - remove build artifacts from tracking'" -ForegroundColor White
Write-Host "  3. After commit, rebuild your project - no more file changes!" -ForegroundColor White
Write-Host ""

