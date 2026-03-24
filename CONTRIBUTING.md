---

# 🤝 Contributing to **Stays**

Thank you for your interest in contributing to **Stays** — a multi‑platform ecosystem for documenting the places people visit and the memories they create.  
We welcome developers, designers, testers, writers, and product thinkers of all experience levels.

This document outlines how to contribute effectively and respectfully across the entire project.

---

## 🧭 Guiding Principles

- **Be thoughtful** — prioritize clarity, maintainability, and user experience.  
- **Be consistent** — follow established patterns for each platform.  
- **Be collaborative** — communicate early, ask questions, and share context.  
- **Be kind** — treat all contributors with respect and empathy.  
- **Be curious** — this project encourages learning and exploration.

---

## 📁 Repository Structure

The Stays monorepo contains:

```
backend/           # ASP.NET Core API
web/
  stays-web-angular/
  stays-web-react/
  stays-web-razor/
mobile/
  stays-ios/       # SwiftUI
  stays-android/   # Kotlin
docs/              # Architecture, API, design, and planning docs
```

Each project has its own README with setup instructions and framework‑specific guidelines.

---

## 🛠️ Getting Started

### 1. **Fork the repository**
Create your own fork to work independently.

### 2. **Clone your fork**
```
git clone https://github.com/<your-username>/stays.git
```

### 3. **Set up the environment**
Follow the setup instructions in each project’s README:

- Backend: .NET SDK, SQL Server  
- Web: Node.js + framework‑specific tooling  
- Mobile: Xcode (iOS), Android Studio (Android)

### 4. **Install dependencies**
Each project includes its own dependency installation steps.

### 5. **Run tests**
Make sure everything passes before you begin development.

---

## 🌱 How to Contribute

### 🧩 1. Pick an Issue or Feature
- Look for issues labeled **good first issue**, **help wanted**, or **enhancement**.  
- If you want to propose something new, open an issue first so we can discuss it.

### 🌿 2. Create a Feature Branch
Use descriptive names:

```
git checkout -b feature/add-photo-tagging
git checkout -b fix/android-location-permissions
```

### 🌳 3. Write Clean, Documented Code
- Follow language‑specific style guides  
- Keep functions small and focused  
- Add comments where clarity helps  
- Update or add tests when appropriate  
- Update documentation if behavior changes  

### 🌲 4. Commit Thoughtfully
Use clear, meaningful commit messages:

```
feat(api): add endpoint for creating stays
fix(ios): resolve crash when uploading photos
docs: update API versioning guidelines
```

### 🌵 5. Keep Pull Requests Focused
A good PR is:

- Small  
- Well‑scoped  
- Tested  
- Documented  
- Easy to review  

Include:

- A summary of the change  
- Screenshots or recordings for UI changes  
- Notes about breaking changes or migrations  

---

## 🧪 Testing Expectations

Each platform has its own testing strategy:

- **Backend:** xUnit, integration tests, API contract tests  
- **Web:** framework‑specific unit tests + end‑to‑end tests  
- **Mobile:** XCTest (iOS), JUnit/Espresso (Android)  

Before submitting a PR:

- All tests must pass  
- New features should include tests  
- Breaking changes must be documented  

---

## 📚 Documentation Standards

When contributing:

- Update `/docs` for architectural or API changes  
- Update project‑level READMEs when setup steps change  
- Add inline comments for complex logic  
- Keep diagrams and schemas current  

---

## 🧵 Code Style Guidelines

### Backend (C#)
- Follow .NET naming conventions  
- Use dependency injection  
- Keep controllers thin; push logic into services  
- Use async/await consistently  
- Avoid static state  

### Angular
- Use standalone components where appropriate  
- Follow Angular style guide  
- Prefer Observables over Promises  

### React
- Use functional components + hooks  
- Keep components small and composable  
- Use TypeScript for type safety  

### Razor Pages
- Keep page models clean  
- Use partials and components for reuse  

### SwiftUI
- Prefer MVVM  
- Keep views declarative and lightweight  
- Use Combine or async/await for data flow  

### Kotlin (Android)
- Use Jetpack libraries  
- Follow MVVM or MVI patterns  
- Use coroutines for async work  

---

## 🗺️ Branching & Release Strategy

- `main` — stable, production‑ready  
- `develop` — active development  
- `feature/*` — new features  
- `fix/*` — bug fixes  
- `docs/*` — documentation updates  

Releases are tagged and documented in the changelog.

---

## 🧑‍🤝‍🧑 Code of Conduct

All contributors must follow our Code of Conduct (coming soon).  
We expect:

- Respectful communication  
- Inclusive behavior  
- Zero tolerance for harassment or discrimination  

---

## 📨 Getting Help

If you’re unsure about something:

- Open a discussion  
- Comment on an issue  
- Ask for clarification in your PR  
- Propose ideas — we love thoughtful suggestions  

We want Stays to be a welcoming place to learn, build, and collaborate.

---

## 🌟 Thank You

Your contributions help shape Stays into a meaningful, multi‑platform experience for users everywhere.  
We’re excited to build this with you.

---
