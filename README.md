# 2024_06_01_HelloMirrorDroneMulti
Just a demo of how can do multiplayer game with Mirror for the Mons workshop.

- https://assetstore.unity.com/packages/tools/network/mirror-129321
- https://github.com/MirrorNetworking/Mirror


- Goal: https://github.com/EloiStree/2024_06_31_DroneRaceStep
- Drone: https://github.com/EloiStree/2023_02_19_RootsOfKnowedgeDrone



-------

For next time I have to explain Mirror

```
🔹 Execution control attributes (where a method is allowed to run)
[Server] → only runs on server, warns if called on client
[ServerCallback] → only runs on server, no warning if client calls it
[Client] → only runs on client, warns if called on server
[ClientCallback] → only runs on client, no warning if server calls it

🔹 Networking synchronization attributes (how data & calls travel between server/clients)

[Command] → client → server call
[ClientRpc] → server → all clients call
[TargetRpc] → server → one client call
[SyncVar] → variable auto-syncs from server → clients
[SyncObject] → collection auto-syncs from server → clients
```
---------------------------
-------------------------------

Le joueur arrive sur le **serveur**, et c’est un **client**. <img width="584" height="240" alt="image" src="https://github.com/user-attachments/assets/aef6a4be-23aa-4052-b3c3-839c1a33776a" />

---

Suis-je bien sur le **serveur** ? <img width="725" height="125" alt="image" src="https://github.com/user-attachments/assets/7911c0d9-5b3a-4a14-b6f7-c8c7f61f81fe" />

---

Suis-je bien le **propriétaire de ce client** ? <img width="1348" height="116" alt="image" src="https://github.com/user-attachments/assets/eb7e482d-f5c3-40c4-95ff-4437332f3d3d" />

---

Chargeons une **clé privée** que moi seul connais, ainsi que la **clé publique** correspondante. <img width="663" height="496" alt="image" src="https://github.com/user-attachments/assets/647a93b0-219b-48db-a07b-898005b78345" />

---

Demandons au serveur de m’envoyer un **message à signer**. <img width="1042" height="228" alt="image" src="https://github.com/user-attachments/assets/3419da34-b9ff-4904-bdcb-c6998d7d5019" />

---

Le serveur demande au client de signer ce message.
Seul le serveur peut demander à un client d’exécuter ce code. <img width="1333" height="493" alt="image" src="https://github.com/user-attachments/assets/1842c5df-24c2-47a4-9c88-9264cd44e811" />

---

Le client doit donc **signer le message** et retourner la **signature**, accompagnée de la commande qui sera exécutée sur le serveur. <img width="1424" height="593" alt="image" src="https://github.com/user-attachments/assets/f5e34a6e-2898-4968-bc52-b27a8658d362" />

---

`IsValid` est une variable **SyncVar** et donc validée chez tous les joueurs. <img width="638" height="72" alt="image" src="https://github.com/user-attachments/assets/91fe674a-4fe9-4524-8bc8-a31e84827039" />

Tout le monde sait ainsi si le joueur est **authentifié ou non**.

De plus, tous les joueurs connaissent la **clé publique** de celui-ci. <img width="402" height="70" alt="image" src="https://github.com/user-attachments/assets/1a28fa40-6090-4910-8b36-e62e7c605192" />

---

Pour ne pas avoir à rechercher notre propre joueur, on l’ajoute à une variable **statique**. <img width="556" height="49" alt="image" src="https://github.com/user-attachments/assets/f41d37aa-e53a-45cc-ba78-a2167f7f96bf" />
