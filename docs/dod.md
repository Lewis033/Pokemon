# Définition de terminé (Definition of Done)

Un récit utilisateur est considéré comme réellement terminé lorsque tous les critères suivants sont respectés :

* Le code compile sans erreur dans Unity.

* La fonctionnalité a été testée manuellement dans l'Éditeur Unity et se comporte comme prévu dans les cas normaux et les cas limites pertinents (ex. : PV à 0, capture réussie/échouée, sac plein).

* Aucune erreur ni exception non gérée n'apparaît dans la console Unity pendant les tests.

* Le code respecte les conventions de nommage C# du projet (PascalCase pour les classes et méthodes, et la convention utilisée par le projet pour les variables locales et les champs privés).

* Le code est commenté aux endroits pertinents (logique non triviale, résumé XML sur les méthodes publiques importantes).

* Le récit répond à tous les critères d'acceptation définis dans le carnet de produit.

* Aucune régression n'est constatée sur les fonctionnalités déjà terminées.

* Le code a été revu (au minimum une relecture) avant d'être implémenté dans la branche principale.

* Le code est poussé sur le dépôt Git avec un historique de commits clair et des messages descriptifs.

* Les nouveaux fichiers et assets ajoutés (images, scènes, prefabs) sont organisés dans les bons répertoires du projet Unity.
