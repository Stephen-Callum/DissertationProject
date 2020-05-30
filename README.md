# [TARTARUS](https://stephen-callum.itch.io/tartarus/devlog/136181/game-launch)

[Link to itch.io page](https://stephen-callum.itch.io/tartarus/devlog/136181/game-launch).

### Using genetic algorithms to develop a new technique for dynamic difficulty adjustment in games.

The aim of using genetic algorithms was to be able to automate the difficulty adjustment process
and be able to apply it to any playerskill level.

![Tartarus](https://imgur.com/Ztg9X7V.jpg)

The player plays as 'Apollo' a scouting ship for the home ship "Prometheus".
![Apollo](https://imgur.com/k73Fy0T.jpg)

Apollo is sent to investigate a distress signal that we come to find out was sent by an enemy home ship 'Zeus'.
Zeus wants to "steal" the technology from Prometheus's scout ship in order to increase their ships power and take Prometheus's resources.

<img src="https://imgur.com/77l4kdJ.jpg" alt="EMPCharge" width="200" height="200"/> <img src="https://imgur.com/aygxdcQ.jpg" alt="CannonFire" width="250" height="250"/> <img src="https://imgur.com/UqKYQlP.jpg" alt="Zeus" width="250" height="250"/>

The scout ship must dodge Zeus's cannon fire to survive while also collecting the purple EMP charges to mount a counterattack.

#### The genetic algorithm and flow

The use of a genetic algorithm was inspired by the pursuit of measuring the effects of a psychological phenomenon called 'Flow' coined by
Csikszentmihályi in 1975. The theory centers around the idea of a mental state in which a person is fully immersed in an activity while also
experiencing heightened focus and enjoyment. He found that there needed to be a balance between the difficulty of the task and the individual's skill level in that task.

![FlowModel](https://imgur.com/ilc9NHW.jpg)

The genetic algorithm in Tartarus was aimed at achieving this balance by measuring the player's skill level and adjusting the difficulty of the game (Zeus's fire rate)
to keep it challenging enough to keep the player interested.

The first ten rounds are randomised, after that the genetic algorithm will start to filter out the best fit difficulties for the player's skill.
