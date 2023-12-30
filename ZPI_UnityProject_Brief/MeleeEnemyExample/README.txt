Here are examples of code for the melee enemy, not all the classes will be shown such as EnemySM and EnemySpecific as the project is still ongoing.

I will provide brief of functionalities of these classes.

EnemySM is a state machine responsible for controlling movement of the enemy. You change its state manually via methods.
This class also provides many methods useful for any type of enemy, such as distance from the player, checking if a path towards selected goal is accesible, getters/setters of speed, acceleration, etc.
This class is also responsible for implementation of dynamic patrols.

EnemySpecific is an abstract class for specific enemy type state machines to inherit from such as MeleeSM, ShamanSM. It provides state machines with both abstract and virtual methods, all that is useful for each enemy type, such as: getting crucial components in AwakeInitialize, providing methods which make use of other components much easier, etc.
The class also serves as a fasade, it allows for easier communications with different components associated with an enemy and provides other parts of the system access to useful public methods without need to know the specific type of enemy, for example: Die(), IsAlive(), GetId().
The class also gives other useful functionalities, such as: functionalities for randomizing state times, functionalities for setting spell probabilities, providing generic death state, etc.

In the project there also RangedEnemy, ShamanEnemy and GolemEnemy.
