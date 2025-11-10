#!/bin/bash
# Bash script to fix .NET .gitignore issues
# Run this script if build artifacts (bin/, obj/, etc.) are being tracked by Git

echo "=== Fixing .NET .gitignore ==="
echo ""

# Step 1: Generate/Update .gitignore with official .NET template
echo "Step 1: Generating official .NET .gitignore..."
dotnet new gitignore --force
if [ $? -eq 0 ]; then
    echo "✓ .gitignore updated"
else
    echo "✗ Failed to generate .gitignore"
    exit 1
fi

echo ""

# Step 2: Ensure wwwroot/lib/ is in .gitignore
echo "Step 2: Checking wwwroot/lib/ in .gitignore..."
if ! grep -q "wwwroot/lib/" .gitignore; then
    echo "" >> .gitignore
    echo "# Client-side libraries (usually restored via LibMan)" >> .gitignore
    echo "wwwroot/lib/" >> .gitignore
    echo "✓ Added wwwroot/lib/ to .gitignore"
else
    echo "✓ wwwroot/lib/ already in .gitignore"
fi

echo ""

# Step 3: Remove already-tracked files that should be ignored
echo "Step 3: Removing build artifacts from Git tracking..."
echo "  (Files will remain on disk, just removed from Git)"

# Remove bin/ and obj/ directories
if [ -d "bin" ]; then
    git rm -r --cached bin/ 2>/dev/null
    echo "✓ Removed bin/ from tracking"
fi

if [ -d "obj" ]; then
    git rm -r --cached obj/ 2>/dev/null
    echo "✓ Removed obj/ from tracking"
fi

# Remove wwwroot/lib/ if it exists
if [ -d "wwwroot/lib" ]; then
    git rm -r --cached wwwroot/lib/ 2>/dev/null
    echo "✓ Removed wwwroot/lib/ from tracking"
fi

echo ""

# Step 4: Stage the .gitignore changes
echo "Step 4: Staging .gitignore changes..."
git add .gitignore
echo "✓ .gitignore staged"

echo ""
echo "=== Done! ==="
echo ""
echo "Next steps:"
echo "  1. Review changes: git status"
echo "  2. Commit: git commit -m 'Fix .gitignore - remove build artifacts from tracking'"
echo "  3. After commit, rebuild your project - no more file changes!"
echo ""

