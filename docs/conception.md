# Conception — diagramme de classe UML

Ce diagramme présente les principales classes et responsabilités du jeu RPG solo de style Pokémon/Game Boy. Il couvre notamment l'exploration, les combats, la capture, la gestion de l'équipe, le stockage dans le PC, la progression des Pokémon, les boutiques et la sauvegarde.

```mermaid
classDiagram

    class GestionnaireJeu {
        -Joueur joueur
        -Combat combatActuel
        +CommencerCombatSauvage(Pokemon pokemonSauvage)
        +CommencerCombatDresseur(Dresseur adversaire)
        +ChargerPartie()
        +SauvegarderPartie()
    }

    class GestionnaireSauvegarde {
        -string cheminSauvegarde
        +SauvegarderJson(Joueur joueur)
        +ChargerJson() Joueur
    }

    class Joueur {
        -Vector2 position
        -Dresseur dresseur
        +Deplacer(Vector2 direction)
        +Interagir()
    }

    class Dresseur {
        -string nom
        -int argent
        -List~Pokemon~ equipe
        -Inventaire sac
        -PCPokemon pc
        +EnvoyerPokemon(int index)
        +EchangerPokemon(int indexEquipe, int indexPC)
        +AjouterPokemonEquipe(Pokemon pokemon)
        +StockerPokemon(Pokemon pokemon)
    }

    class Pokemon {
        -string surnom
        -int niveau
        -int experience
        -int pointsDeVieActuels
        -EspecePokemon espece
        -List~Capacite~ capacites
        +GagnerExperience(int experience)
        +MonterDeNiveau()
        +Evoluer()
        +ApprendreCapacite(Capacite capacite)
        +SubirDegats(int degats)
        +EtreKO()
    }

    class EspecePokemon {
        -string nomEspece
        -string type
        -int pointsDeVieDeBase
        -int attaqueDeBase
        -EspecePokemon especeEvolution
        -int niveauEvolution
    }

    class Capacite {
        -string nom
        -string type
        -int puissance
        -int pointsPouvoirActuels
        +Utiliser(Pokemon cible)
    }

    class Combat {
        -Dresseur joueur
        -Dresseur adversaire
        -Pokemon pokemonSauvage
        +ResoudreTour(Capacite capaciteChoisie)
        +VerifierFinCombat()
        +TenterCapture(Pokemon pokemonSauvage)
        +DonnerRecompense()
    }

    class Inventaire {
        -List~Objet~ objets
        +AjouterObjet(Objet objet)
        +UtiliserObjet(Objet objet, Pokemon cible)
    }

    class Objet {
        -string nom
        +Utiliser(Pokemon cible)
    }

    class PCPokemon {
        -List~Pokemon~ pokemonStockes
        +StockerPokemon(Pokemon pokemon)
        +RetirerPokemon(int index)
    }

    class Boutique {
        -string nom
        -List~ArticleBoutique~ articles
        +AcheterObjet(Dresseur dresseur, ArticleBoutique article)
    }

    class ArticleBoutique {
        -Objet objet
        -int prix
    }

    class Zone {
        -string nom
        -List~EspecePokemon~ pokemonDisponibles
        +GenererRencontre()
    }

    class DresseurNPC {
        -Vector2 position
        +Deplacer()
        +Interagir(Joueur joueur)
    }

    class PNJ {
        -string nom
        -string dialogue
        +Interagir(Joueur joueur)
    }


    GestionnaireJeu "1" --> "1" Joueur : contrôle
    GestionnaireJeu "1" --> "1" GestionnaireSauvegarde : utilise
    GestionnaireJeu "1" --> "0..1" Combat : lance

    Joueur "1" *-- "1" Dresseur : contrôle
    Joueur "1" --> "*" Zone : explore
    Joueur "1" --> "*" PNJ : interagit
    Joueur "1" --> "*" DresseurNPC : rencontre

    Dresseur "1" *-- "0..6" Pokemon : équipe
    Dresseur "1" *-- "1" Inventaire : possède
    Dresseur "1" *-- "1" PCPokemon : possède
    Dresseur "1" --> "*" Boutique : visite

    Pokemon "1" o-- "1" EspecePokemon : possède
    Pokemon "1" *-- "1..4" Capacite : possède

    EspecePokemon "0..1" --> "0..1" EspecePokemon : évolue vers

    Inventaire "1" o-- "*" Objet : contient
    PCPokemon "1" o-- "*" Pokemon : stocke

    Combat "1" --> "1" Dresseur : joueur
    Combat "1" --> "0..1" Dresseur : adversaire
    Combat "1" --> "0..1" Pokemon : Pokémon sauvage

    Boutique "1" *-- "*" ArticleBoutique : vend
    ArticleBoutique "1" --> "1" Objet : représente

    Zone "1" o-- "*" EspecePokemon : permet de rencontrer

    DresseurNPC "1" --> "1" Dresseur : possède
    DresseurNPC "*" --> "*" Zone : se déplace dans
```

## Description des principales classes

### G
