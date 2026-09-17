# Pokemon


## Projet créé par :
- Louis-Philip Duclos
- Arthur Buyens

## Jeu dans le style Pokémon :
L'intention de ce projet est de développer un petit jeu vidéo inspiré de l'univers Pokémon, dans lequel le joueur incarne un dresseur explorant un monde rempli de créatures sauvages.
Le but est de capturer, entraîner et faire combattre ces créatures afin de progresser à travers différentes zones et devenir le meilleur dresseur possible.
Le projet vise avant tout à mettre en pratique les notions de programmation orientée objet et de gestion d'état de jeu, tout en offrant une expérience simple mais amusante.

### Les principales fonctionnalités que nous comptons développer sont :
- Exploration d'une carte du monde composée de plusieurs zones (forêt, ville, route, etc.)
- Capture de créatures sauvages rencontrées aléatoirement
- Système de combat au tour par tour entre créatures (attaques, points de vie, types)
- Gestion d'une équipe de créatures avec statistiques et niveaux
- Inventaire contenant des objets utiles (potions, Pokeballs, etc.)
- Système de progression (montée de niveau, évolution des créatures)
- Sauvegarde et chargement de la partie en cours

## Choix technologiques

| Type | Choix | Justification | Licence |
|---|---|---|---|
| Langage de programmation | C# | Langage principalement utilisé avec Unity et bien documenté ; son typage fort est adapté à la logique du jeu (combat, inventaire). | C# : ECMA/ISO ; .NET : MIT |
| Cadre applicatif | Unity Engine | Moteur de jeu 2D complet (rendu, boucle de jeu, gestion des scènes) adapté à un RPG de style Game Boy ; large communauté et documentation. | Propriétaire — Unity Personal |
| Bibliothèques et choix de l'IUG | Unity UI (uGUI) | Solution d'interface intégrée à Unity, suffisante pour des menus simples (combat, sac, équipe) sans dépendance externe. | Incluse avec Unity |
| Plateforme d'exécution | Unity Player — build Windows (Standalone) | Cible simple pour un projet de cours ; permet d'exécuter le jeu directement sur Windows. | Incluse avec Unity |
| Format de persistance des données | JSON via `JsonUtility` (Unity) | Jeu solo, pas besoin d'une base de données ; `JsonUtility` est inclus nativement dans Unity et suffit pour sauvegarder l'équipe et l'inventaire du joueur. | Incluse avec Unity |
| Atelier de développement (IDE) | Visual Studio Community + Unity Editor | Intégration avec Unity pour le débogage et l'autocomplétion C# ; permet de développer et tester efficacement le projet. | Visual Studio Community : licence Microsoft |
| Gestionnaire du dépôt de code source | Git + GitHub | Suivi de version standard ; permet le travail collaboratif et le suivi des modifications du projet. | Git : GNU GPLv2 ; GitHub : service propriétaire |