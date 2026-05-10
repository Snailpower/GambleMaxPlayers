# DevOps Guidelines

## Branch Strategy

### Protected Branches

| Branch | Description |
|--------|-------------|
| `master` | Production-ready code |

### Branch Rules

#### `master`
- Merges require a **Pull Request** with **at least 1 approval**
- Only `feature/*` and `bugfix/*` branches may be merged into `master`
- Direct pushes are not allowed

---

## Branch Naming Conventions

All branches must follow these naming patterns:

```
feature/<issue-number>-short-description
bugfix/<issue-number>-short-description
```

### Examples

| Type | Example |
|------|---------|
| Feature | `feature/42-add-shop-system` |
| Bug fix | `bugfix/17-fix-player-spawn` |
| No issue | `feature/0-fix-typo-in-readme` |

### Rules
- Use **lowercase** and **hyphens** only (no spaces or underscores)
- Always include the **issue number** after the type prefix; use `0` when there is no associated issue
- Keep the description short and meaningful

---

## Commit Message Rules

All commit messages must follow this format:

```
#<issue-number> short description of the change
```

### Examples

| Type | Example |
|------|---------|
| Feature | `#42 add shop system` |
| Bug fix | `#17 fix player spawn position` |
| No issue | `#no-issue fix typo in readme` |

### Rules
- Always start with the **issue number** prefixed by `#`
- Use `#no-issue` when there is no associated issue
- Use **lowercase** for the description
- Keep the description short and meaningful
- Do not end with a period

---

## Pull Request Guidelines

- PR titles should follow this structure: 
```
#<issue-number> short description of what the PR adds/changes/removes
```
- PRs into `master` require **1 approval** before merging
- Link the relevant issue in the PR description
- Ensure the branch is up to date with `master` before merging
- Delete the source branch after merging to keep the branch list clean

---

## Release Management

### Issue Tagging

- All GitHub issues that are intended for a release must be **tagged with the corresponding release label** (e.g., `release/1.0.0`)
- When setting up a release, all issues carrying the release tag are bundled into that release
- Before a release is created, all tagged issues must be **merged and ready on `master`**

### Release Checklist

- [ ] All issues for the release are tagged with the release label
- [ ] All tagged issues are merged into `master`
- [ ] `master` is stable and passing all checks
