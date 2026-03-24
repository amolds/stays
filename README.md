---

# 🏡 **Stays**  
### *Capture where you’ve been. Remember why it mattered.*

Stays is a cross‑platform application ecosystem designed to help people document the places they visit — from weekend getaways to once‑in‑a‑lifetime adventures. Users can track locations, attach photos, write notes, and store meaningful details that turn simple trips into lasting memories.

This repository serves as the **top‑level monorepo** for the entire Stays platform, including backend services, web applications, and native mobile apps.

---

## 🌟 **Purpose & Vision**

Stays exists to make personal travel history meaningful, organized, and accessible. Whether someone wants to remember a favorite restaurant, track national parks visited, or document a multi‑country journey, Stays provides a unified place to store and revisit those experiences.

Our vision is to create:

- A **beautiful, intuitive user experience** across all platforms  
- A **robust, scalable backend** that supports rich media and structured data  
- A **flexible architecture** that allows developers to explore multiple UI frameworks  
- A **platform for future expansion**, such as social sharing, trip planning, or AI‑powered insights  

---

## 🧱 **High‑Level Architecture**

Stays is intentionally built as a multi‑client ecosystem to explore different technologies while sharing a common backend.

### **Backend**
- **Language:** C#  
- **Framework:** ASP.NET Core Web API  
- **Database:** SQL Server  
- **Purpose:**  
  - Centralized API for all clients  
  - Authentication & authorization  
  - Storage of visits, photos, notes, metadata  
  - Documentation endpoints for future integrations  

### **Web Applications**
We maintain three separate web clients to explore different frontend paradigms:

| Web App | Framework | Purpose |
|--------|-----------|---------|
| `stays-web-angular` | Angular | Full‑featured SPA with strong structure |
| `stays-web-react` | React | Flexible, component‑driven SPA |
| `stays-web-razor` | Razor Pages | Server‑rendered simplicity and performance |

Each web app consumes the same backend API.

### **Mobile Applications**
Two native mobile apps provide a first‑class mobile experience:

| Mobile App | Platform | Framework |
|------------|----------|-----------|
| `stays-ios` | iOS | SwiftUI |
| `stays-android` | Android | Kotlin |

These apps support offline caching, photo uploads, and location services.

---

## 📦 **Repository Structure**

```
/stays
 ├── backend/
 │    └── stays-api/            # ASP.NET Core Web API
 │
 ├── web/
 │    ├── stays-web-angular/    # Angular SPA
 │    ├── stays-web-react/      # React SPA
 │    └── stays-web-razor/      # Razor Pages app
 │
 ├── mobile/
 │    ├── stays-ios/            # SwiftUI app
 │    └── stays-android/        # Kotlin app
 │
 ├── docs/                      # Architecture, API docs, design notes
 └── README.md                  # You're here
```

---

## 🎯 **Project Goals**

### **For Users**
- Track places visited with rich detail  
- Add photos, notes, tags, and documentation  
- Browse memories across devices  
- Enjoy a consistent, polished experience  

### **For Developers**
- Explore multiple frontend frameworks  
- Practice building a shared API consumed by diverse clients  
- Learn native mobile development patterns  
- Contribute to a real, multi‑platform product  

### **For Product Owners**
- Maintain a clear roadmap  
- Support iterative feature delivery  
- Enable experimentation without compromising core functionality  
- Build a foundation for future monetization or social features  

---

## 🧭 **Guiding Principles**

### **1. Consistency Across Platforms**
Each client should feel like “Stays,” even if built with different technologies.

### **2. API‑First Development**
Backend contracts should be stable, documented, and versioned.

### **3. Developer Learning & Exploration**
This project encourages experimentation — but with structure and intention.

### **4. Quality Over Speed**
We value maintainability, clarity, and thoughtful design.

### **5. User‑Centered Design**
Every feature should make documenting and revisiting memories delightful.

---

## 🛠️ **Tech Stack Overview**

| Layer | Technology |
|-------|------------|
| Backend | C#, ASP.NET Core, SQL Server |
| Web | Angular, React, Razor Pages |
| Mobile | SwiftUI, Kotlin |
| DevOps (future) | CI/CD pipelines, containerization, cloud hosting |
| Documentation | Markdown, OpenAPI/Swagger |

---

## 📚 **Documentation**

All architecture diagrams, API references, and design guidelines live in the `/docs` directory.  
This includes:

- API specification  
- Database schema  
- Feature roadmap  
- UI/UX guidelines  
- Contribution standards  

---

## 🤝 **Contributing**

We welcome contributions from developers of all experience levels.  
Please follow these guidelines:

- Use feature branches  
- Write clear commit messages  
- Follow language‑specific style guides  
- Keep code modular and testable  
- Document new endpoints or components  

A full CONTRIBUTING.md will be added soon.

---

## 🧪 **Testing Strategy**

Each platform includes its own testing approach:

- **Backend:** xUnit, integration tests, API contract tests  
- **Web:** Framework‑specific unit tests + end‑to‑end tests  
- **Mobile:** XCTest (iOS), JUnit/Espresso (Android)  

---

## 🚀 **Roadmap (High‑Level)**

- MVP:  
  - User accounts  
  - Create/view/edit/delete stays  
  - Photo uploads  
  - Notes & tags  
  - Basic map integration  

- Phase 2:  
  - Offline support for mobile  
  - Trip grouping  
  - Search & filtering  
  - Export/import  

- Phase 3:  
  - Social features  
  - Shared trips  
  - AI‑powered suggestions  
  - Public profiles  

---

## ❤️ **Why Stays Matters**

Memories fade — but the places that shape us deserve to be remembered.  
Stays helps people preserve their journeys, reflect on their experiences, and revisit the moments that made them who they are.

---