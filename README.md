# DealHub

> Plateforme communautaire de deals — trouvez, partagez et votez les meilleures offres du moment.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind-4.x-06B6D4?logo=tailwindcss)](https://tailwindcss.com/)
[![SignalR](https://img.shields.io/badge/SignalR-realtime-512BD4)](https://dotnet.microsoft.com/apps/aspnet/signalr)

---

## Sommaire

1. [Présentation](#-présentation)
2. [Fonctionnalités](#-fonctionnalités)
3. [Stack technique](#-stack-technique)
4. [Architecture](#-architecture)
5. [Prérequis](#-prérequis)
6. [Installation](#-installation)
7. [Démarrage rapide](#-démarrage-rapide)
8. [Configuration](#-configuration)
9. [Lancer le projet](#-lancer-le-projet)
10. [Rôles utilisateurs](#-rôles-utilisateurs)
11. [Travailler avec Git](#-travailler-avec-git)
12. [Conventions de code](#-conventions-de-code)
13. [Tâches en cours](#-tâches-en-cours)
14. [Contributeurs](#-contributeurs)

---

## 🎯 Présentation

DealHub est une application web communautaire inspirée de Dealabs. Elle permet aux utilisateurs de :

- Publier et découvrir les meilleures offres et promotions
- Voter **Chaud 🔥** ou **Froid ❄️** pour évaluer la qualité d'un deal
- Recevoir des alertes personnalisées par mot-clé, catégorie ou marchand
- Commenter et interagir avec la communauté en temps réel
- Accéder à un espace marchand pour soumettre des offres sponsorisées

---

## ✨ Fonctionnalités

### À implémenter

- [ ] Authentification (email/mot de passe + OAuth Google & GitHub)
- [ ] Gestion des rôles : Visiteur, Utilisateur, Marchand, Modérateur, Administrateur
- [ ] Publication et gestion de deals
- [ ] Système de votes Chaud / Froid avec mise à jour temps réel (SignalR)
- [ ] Commentaires avec réponses imbriquées (1 niveau)
- [ ] Signalement de deals et commentaires inappropriés
- [ ] File de modération avec escalade automatique
- [ ] Système d'alertes (mot-clé, catégorie, marchand) par email et push
- [ ] Notifications temps réel via SignalR
- [ ] Système de réputation et badges (🌱 → 👑)
- [ ] Dashboard utilisateur, marchand, modérateur et administrateur
- [ ] Deals sponsorisés avec statistiques de campagne
- [ ] Demande et validation de compte marchand
- [ ] Expiration automatique des deals (background service)
- [ ] Partage de deals sur les réseaux sociaux
- [ ] Recherche avancée avec filtres multiples
- [ ] Export RGPD des données utilisateur
- [ ] Tests unitaires et d'intégration

---

## 🛠 Stack technique

| Couche | Technologie |
|---|---|
| Framework | ASP.NET Core 9 MVC |
| ORM | Entity Framework Core 9 |
| Base de données | SQL Server |
| Temps réel | SignalR |
| Frontend | Razor Views + Tailwind CSS 4 |
| Authentification | ASP.NET Identity + OAuth 2.0 |
| Logs | Serilog |
| Icônes | Iconify (pack Solar) |

---

## 🏗 Architecture

Le projet suit une architecture **N-Tiers** en 4 couches :

```
DealHub.sln
├── DealHub.Domain          # Entités, enums, interfaces
├── DealHub.Infrastructure  # EF Core, repositories, background services
├── DealHub.Application     # Services métier
└── DealHub.Web             # Controllers, Razor Views, SignalR Hubs, JS/CSS
```

### Entités principales

```
Utilisateur ──── Deal ──────── Vote
     │             │
     │             ├── Commentaire
     │             └── Signalement
     │
     ├── Alerte
     ├── Notification
     ├── FavoriDeal
     └── CompteMarchand ── Marchand
```

---

## 📋 Prérequis

Avant de commencer, assurez-vous d'avoir installé :

| Outil | Version minimale | Lien |
|---|---|---|
| .NET SDK | 9.0 | https://dotnet.microsoft.com/download |
| SQL Server | 2022 (ou Express) | https://www.microsoft.com/sql-server |
| Node.js | 22+ (pour Tailwind CLI) | https://nodejs.org |
| Git | 2.x | https://git-scm.com |

---

## 🚀 Installation

### 1. Cloner le dépôt

```
git clone https://github.com/Jorvan-Kleiv/DealHub.git
```

### 2. Restaurer les dépendances .NET

```
dotnet restore
```

### 3. Installer les dépendances Node (Tailwind)

```
cd DealHub.Web
npm install
```

### 4. Appliquer les migrations Entity Framework

Dans la **Console du Gestionnaire de Packages** (Package Manager Console) de Visual Studio :

```
Update-Database
```

---

## ⚡ Démarrage rapide

> À effectuer **à chaque fois** que vous ouvrez le projet.

### Terminal 1 — Tailwind (CSS en temps réel)

Ouvrez un terminal dans le dossier `DealHub.Web` et lancez :

```
npm run watch
```

Laissez ce terminal ouvert. Tailwind recompilera automatiquement le CSS à chaque modification.

### Terminal 2 — Application .NET

Ouvrez un second terminal dans le dossier `DealHub.Web` et lancez :

```
dotnet watch run
```

L'application sera disponible sur :
- **HTTPS** : https://localhost:7051
- **HTTP** : http://localhost:5051

> ⚠️ Les deux terminaux doivent rester ouverts en même temps pendant le développement.

### En cas de problème au démarrage

**Certificat HTTPS invalide :**
```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

**Cache de build corrompu :**
```
dotnet clean
dotnet restore
dotnet watch run
```

**Erreur de migration :**
Dans la PMC Visual Studio :
```
Drop-Database
Update-Database
```

---

## ⚙️ Configuration

> ⚠️ **Chaque développeur gère son propre fichier `appsettings.Development.json` en local.**
> Ce fichier est dans le `.gitignore` — **ne jamais le committer et ne jamais toucher au fichier de configuration des autres.**

Créez votre fichier `DealHub.Web/appsettings.Development.json` avec vos valeurs locales :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DealHub;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Authentication": {
    "Google": {
      "ClientId": "VOTRE_GOOGLE_CLIENT_ID",
      "ClientSecret": "VOTRE_GOOGLE_CLIENT_SECRET"
    },
    "GitHub": {
      "ClientId": "VOTRE_GITHUB_CLIENT_ID",
      "ClientSecret": "VOTRE_GITHUB_CLIENT_SECRET"
    }
  },
  "EmailSettings": {
    "SmtpHost": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "noreply@dealhub.fr",
    "SenderPassword": "VOTRE_MOT_DE_PASSE_APPLICATION"
  },
  "AdminSeed": {
    "Email": "VOTRE_EMAIL_ADMIN",
    "Password": "VOTRE_MOT_DE_PASSE_ADMIN",
    "FirstName": "Admin",
    "LastName": "DealHub"
  },
  "Serilog": {
    "MinimumLevel": "Debug"
  }
}
```

---

## 👥 Rôles utilisateurs

| Rôle | Accès | Attribution |
|---|---|---|
| **Visiteur** | Lecture seule (deals, recherche) | Automatique (non connecté) |
| **Utilisateur** | Votes, commentaires, alertes, favoris | Automatique à l'inscription |
| **Marchand** | Deals sponsorisés + stats campagnes | Demande validée par admin |
| **Modérateur** | File de signalements, modération | Nommé par administrateur |
| **Administrateur** | Accès total + configuration | Seed ou promu par admin |

---

## 🌿 Travailler avec Git

### Workflow général

```
main          ← branche de production (protégée)
└── develop   ← branche d'intégration
    └── prenom-nom   ← une branche par développeur
```

### Étapes pour contribuer

**1. Mettre à jour votre develop local**

```
git checkout develop
git pull origin develop
```

**2. Se placer sur votre branche personnelle**

```
git checkout prenom-nom
```

Si votre branche n'existe pas encore :

```
git checkout -b prenom-nom
git push -u origin prenom-nom
```

**3. Committer régulièrement**

```
git add .
git commit -m "Description des changements apportés"
```

**4. Pousser votre branche**

```
git push origin prenom-nom
```

**5. Ouvrir une Pull Request vers `develop` sur GitHub quand la tâche est terminée.**

### ⚠️ Règles importantes

- Ne jamais pousser directement sur `main` ou `develop`
- Ne jamais modifier le fichier `appsettings.Development.json` d'un autre développeur
- Toujours faire un `git pull` sur `develop` avant de commencer à travailler

---

## 📏 Conventions de code

- **Langue** : code en **anglais**, UI et messages utilisateur en **français**
- **Nommage C#** : PascalCase pour les classes et méthodes, camelCase pour les variables
- **Controllers** : suffixe `Controller`, routes en kebab-case
- **Async** : toujours suffixe `Async` sur les méthodes asynchrones

---

## 📌 Tâches en cours

| Tâche | Statut | Assigné à |
|---|---|---|
| Module alertes | 🔴 À faire | — |
| Intégration marchands (Amazon, Alibaba...) | 🔴 À faire | — |
| Gestion rôles & permissions | 🔴 À faire | — |
| Votes temps réel (SignalR) | 🔴 À faire | — |
| Commentaires | 🔴 À faire | — |
| Signalement deals & commentaires | 🔴 À faire | — |
| Partage réseaux sociaux | 🔴 À faire | — |
| Recherche avancée | 🔴 À faire | — |
| Expiration automatique des deals | 🔴 À faire | — |
| Demande compte marchand | 🔴 À faire | — |
| Refactoring général | 🔴 À faire | — |
| Tests unitaires | 🔴 À faire | — |

---

## 👨‍💻 Contributeurs

| Nom | Rôle |
|---|---|
| Jorvan Kleiv | Lead Developer |

---

<div align="center">
  <p>Fait avec ❤️ par l'équipe DealHub</p>
  <p><strong>DealHub</strong> — La communauté des bonnes affaires 🔥</p>
</div>