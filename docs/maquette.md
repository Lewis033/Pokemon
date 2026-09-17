# Maquette — description visuelle sommaire des écrans principaux

Fil de fer textuel des écrans clés du jeu (RPG style Game Boy, inspiré de Pokémon).

Les maquettes présentent la disposition générale des éléments principaux de chaque écran. Les images ou captures de référence, si nécessaires, sont placées dans `docs/images/`.

## 1. Écran titre

```text
+--------------------------------------+
|                                      |
|             NOM DU JEU               |
|          (logo / illustration)       |
|                                      |
|                                      |
|          > Nouvelle partie           |
|            Continuer                 |
|            Options                   |
|                                      |
+--------------------------------------+
```

* Menu vertical navigable au clavier ou à la manette.
* Le joueur peut sélectionner une nouvelle partie ou continuer une partie sauvegardée.
* « Continuer » est désactivé s'il n'existe aucune sauvegarde.

## 2. Écran carte du monde (overworld)

```text
+--------------------------------------+
| [herbe][herbe][arbre][herbe][chemin] |
| [herbe][ PJ >][herbe][herbe][herbe] |
| [chemin][herbe][PNJ][herbe][herbe]  |
| [arbre][chemin][herbe][herbe][arbre] |
+--------------------------------------+
```

* Vue du dessus inspirée des RPG 2D de style Game Boy.
* Le joueur se déplace sur la carte avec les touches directionnelles ou la manette.
* Le joueur et les dresseurs peuvent se déplacer dans les différentes zones.
* Entrer dans l'herbe haute peut déclencher une rencontre avec un Pokémon sauvage.
* Interagir avec un PNJ ou un dresseur permet de déclencher une interaction ou un combat.
* Le menu principal permet notamment d'accéder à l'équipe, au sac et au PC.

## 3. Écran de combat

```text
+--------------------------------------+
|                                      |
|             Pokémon adverse          |
|             PV [========  ]          |
|                                      |
|     Pokémon du joueur                |
|     PV [======    ] 14/20            |
|--------------------------------------|
| Que doit faire POKEMON ?             |
| > ATTAQUE       SAC                  |
|   POKEMON       FUITE                |
+--------------------------------------+
```

* Vue latérale avec le Pokémon du joueur et le Pokémon adverse.
* Les barres de PV et les valeurs numériques sont affichées.
* Le menu d'action contient quatre choix : Attaque, Sac, Pokémon et Fuite.
* « Attaque » ouvre la liste des capacités du Pokémon actif.
* « Sac » permet d'utiliser un objet, notamment une Poké Ball pour tenter de capturer un Pokémon sauvage.
* « Pokémon » permet de changer le Pokémon actif.
* Une victoire contre un dresseur peut donner de l'argent au joueur.

## 4. Écran équipe et sac

```text
+--------------------------------------+
| ÉQUIPE              | SAC            |
|---------------------|----------------|
| 1. Pokémon A        | Potion      x3  |
| 2. Pokémon B        | Poké Ball   x5  |
| 3. Pokémon C        | Antidote    x1  |
| 4. Pokémon D        |               |
| 5. Pokémon E        |               |
| 6. Pokémon F        |               |
+--------------------------------------+
```

* Accessible depuis le menu de l'overworld.
* La section Équipe affiche les Pokémon actuellement utilisés par le joueur.
* Les informations d'un Pokémon, comme son niveau, ses PV et son expérience, peuvent être consultées en le sélectionnant.
* Le joueur peut consulter les informations d'un Pokémon et réorganiser son équipe.
* Le joueur peut échanger un Pokémon de son équipe avec un Pokémon stocké dans le PC.
* La section Sac affiche les objets possédés et leur quantité.
* Les objets peuvent être utilisés sur les Pokémon lorsque leur utilisation est permise.

## 5. Écran PC Pokémon

```text
+--------------------------------------+
|              PC POKÉMON              |
|--------------------------------------|
| 1. Pokémon A      Niveau 12          |
| 2. Pokémon B      Niveau 18          |
| 3. Pokémon C      Niveau 7           |
| 4. Pokémon D      Niveau 15          |
| 5. Pokémon E      Niveau 10          |
|                                      |
| > Retirer       Retour               |
+--------------------------------------+
```

* Accessible depuis un point de stockage prévu sur la carte.
* Affiche les Pokémon qui ne font pas partie de l'équipe active.
* Le joueur peut retirer un Pokémon du PC pour l'ajouter à son équipe.
* Un Pokémon capturé peut être envoyé automatiquement dans le PC si l'équipe contient déjà six Pokémon.
* Le joueur peut échanger un Pokémon de son équipe avec un Pokémon stocké.

## 6. Écran de montée de niveau et d'évolution

```text
+--------------------------------------+
|                                      |
|          Pokémon a gagné             |
|             un niveau !              |
|                                      |
|        Niveau 5  ->  Niveau 6        |
|                                      |
|             XP [========]             |
|                                      |
|       Votre Pokémon évolue !         |
|                                      |
|          [Ancienne forme]            |
|                 ↓                    |
|          [Nouvelle forme]            |
|                                      |
|              > Confirmer             |
+--------------------------------------+
```

* Lorsqu'un Pokémon accumule suffisamment d'expérience, il monte de niveau.
* Les statistiques du Pokémon peuvent être mises à jour lors de la montée de niveau.
* Une évolution peut être déclenchée lorsqu'une condition d'évolution est remplie, par exemple un niveau donné.
* Une nouvelle capacité peut également être proposée lors d'une montée de niveau.
* Le joueur confirme l'évolution lorsque celle-ci est déclenchée.
