# Quick Fix Commands for .NET .gitignore Issues

If build artifacts (`bin/`, `obj/`, `wwwroot/lib/`) are showing up as changed files after rebuilding, run these commands:

## Option 1: Use the Script (Recommended)

### Windows PowerShell:
```powershell
.\fix-gitignore.ps1
```

### Linux/Mac/Git Bash:
```bash
chmod +x fix-gitignore.sh
./fix-gitignore.sh
```

## Option 2: Manual Commands (Copy & Paste)

### Step 1: Generate/Update .gitignore
```bash
dotnet new gitignore --force
```

### Step 2: Add wwwroot/lib/ to .gitignore (if not already there)
```bash
# Check if it exists
grep -q "wwwroot/lib/" .gitignore || echo -e "\n# Client-side libraries\nwwwroot/lib/" >> .gitignore
```

### Step 3: Remove tracked files from Git (keeps files on disk)
```bash
git rm -r --cached bin/
git rm -r --cached obj/
git rm -r --cached wwwroot/lib/
```

### Step 4: Stage .gitignore
```bash
git add .gitignore
```

### Step 5: Commit the changes
```bash
git commit -m "Fix .gitignore - remove build artifacts from tracking"
```

## One-Liner (PowerShell)
```powershell
dotnet new gitignore --force; if (-not (Select-String -Path .gitignore -Pattern "wwwroot/lib/")) { Add-Content .gitignore "`n# Client-side libraries`nwwwroot/lib/" }; git rm -r --cached bin/ obj/ wwwroot/lib/ 2>$null; git add .gitignore; Write-Host "Done! Now run: git commit -m 'Fix .gitignore'"
```

## One-Liner (Bash)
```bash
dotnet new gitignore --force && grep -q "wwwroot/lib/" .gitignore || echo -e "\n# Client-side libraries\nwwwroot/lib/" >> .gitignore && git rm -r --cached bin/ obj/ wwwroot/lib/ 2>/dev/null && git add .gitignore && echo "Done! Now run: git commit -m 'Fix .gitignore'"
```

## For New Projects (Prevention)

**Always run this BEFORE your first commit:**
```bash
dotnet new gitignore
```

Or initialize Git after creating the project so build artifacts are never tracked.

