# Human-Motion-AI
Human-Motion-AI aims to simulate human movement behavior, using artificial intelligence. The Unity project is based on the [walker simulation](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Learning-Environment-Examples.md#walker) from the [Machine Learning Agents Toolkit](https://github.com/Unity-Technologies/ml-agents) (also known as ML-Agents) and incorporates various optimizations. 

**Note that this documentation keeps track of all steps performed, including those that were unsuccessful. This allows inexperienced developers to evaluate and understand the procedure and gain new knowledge. Parts of my learning progress in machine learning, specifically about reinforcement learning in Unity, are captured in this documentation and is intended to be helpful for developers working on similar projects. My recommendation is to read the complete documentation first, preventing avoidable errors. Background knowledge, helpful tips and links are included. If you want to dive in directly, be sure to pay close attention to the [installation and version instructions](https://github.com/georghauschild/AIreadmeTest#installation-and-version-instructions).**

## Index
<!--- [Chronological Order](https://github.com/georghauschild/AIreadmeTest#chronological-order) -->
- [Steps of Optimization](https://github.com/georghauschild/Human-Motion-AI#steps-of-optimization)
- [Training](https://github.com/georghauschild/Human-Motion-AI#training)
  - [Hardware Utilization](https://github.com/georghauschild/Human-Motion-AI#hardware-utilization)
  - [Customized Training Routines](https://github.com/georghauschild/Human-Motion-AI#customized-training-routines)
    - [Attempt 1 - Increased Agent Number](https://github.com/georghauschild/Human-Motion-AI#attempt-1---increased-agent-number)
    - [Attempt 2 - Concurrent Unity Instances](https://github.com/georghauschild/Human-Motion-AI#attempt-2---concurrent-unity-instances)
    - [Attempt 3 - More Simultaneous Instances and Hidden Units Changes](https://github.com/georghauschild/Human-Motion-AI#attempt-3---more-simultaneous-instances-and-hidden-units-changes)
- [Developer Branch Integration](https://github.com/georghauschild/Human-Motion-AI#developer-branch-integration)
- [Developer Branch AI model](https://github.com/georghauschild/Human-Motion-AI#developer-branch-ai-model)
- [Imitation Learning](https://github.com/georghauschild/Human-Motion-AI#imitation-learning)
    - [Gail and BC](https://github.com/georghauschild/Human-Motion-AI#gail-and-bc)
    - [BC](https://github.com/georghauschild/Human-Motion-AI#bc)
- [Installation and Version Instructions](https://github.com/georghauschild/Human-Motion-AI#installation-and-version-instructions)
  - [Installation Advice](https://github.com/georghauschild/Human-Motion-AI#hardware-utilization)
  - [Library Versions](https://github.com/georghauschild/Human-Motion-AI/tree/main#library-versions)
- [Scripts](https://github.com/georghauschild/Human-Motion-AI/tree/main#scripts)
  - [Walker Agent](https://github.com/georghauschild/Human-Motion-AI#walker-agent)
  - [Decision Requester](https://github.com/georghauschild/Human-Motion-AI#decision-requester)
  - [Demonstration File](https://github.com/georghauschild/Human-Motion-AI#demonstration-file)
  - [Generate AI Model](https://github.com/georghauschild/Human-Motion-AI#generate-ai-model)

<!---
## Chronological Order
1. [Installation and Version Instructions](https://github.com/georghauschild/AIreadmeTest#installation-and-version-instructions)
2. [Steps of Optimization](https://github.com/georghauschild/AIreadmeTest#steps-of-optimization)
3. [Training - Hardware Utilization](https://github.com/georghauschild/AIreadmeTest#hardware-utilization)
4. [Training - Customized Training Routines - Attempt 1 - Increased Agent Number](https://github.com/georghauschild/AIreadmeTest#attempt-1---increased-agent-number)
5. [Training - Customized Training Routines - Attempt 2 - Concurrent Unity Instances](https://github.com/georghauschild/AIreadmeTest#attempt-2---concurrent-unity-instances)
6. [Training - Customized Training Routines - Attempt 3 - More Simultaneous Instances and Hidden Units Changes - Part One](https://github.com/georghauschild/AIreadmeTest#attempt-3---more-simultaneous-instances-and-hidden-units-changes)
7. [Configuration of the Neural Network - Configuration Modification 1 Reduced Hidden Units](https://github.com/georghauschild/AIreadmeTest#configuration-modification-1---reduced-hidden-units)
8. [Training - Customized Training Routines - Attempt 3 - More Simultaneous Instances and Hidden Units Changes - Part Two](https://github.com/georghauschild/AIreadmeTest#part-two)
9. [Developer Branch Integration](https://github.com/georghauschild/AIreadmeTest#developer-branch-integration)
10. [Developer Branch AI Model](https://github.com/georghauschild/AIreadmeTest/blob/main/README.md#developer-branch-ai-model)
10. ...to be continued
-->

## Steps of Optimization
- [x] Training
- [x] Configuration of the Neural Network
- [x] Script Modifications
- [x] Developer Branch Integration
- [x] Developer Branch AI model
- [x] Imitation Learning
- [ ] Support from non-AI-based Tools
- [ ] Additional Human Movements
- [ ] Implementation of [Marathon](https://github.com/Unity-Technologies/marathon-envs)

The project is work in progress. Unprocessed optimization methods may be subject of change.  
- [x] indicates that the improvement method has already been evaluated and potentially implemented
- [ ] indicates that the processing is still pending 

## Training
In order to generate a custom AI model, it is advisable to first optimize the training process and tailor it to the available hardware.

### Hardware Utilization
Following components were used, creating Human-Motion-AI:
- CPU: AMD Ryzen 9-5900X[^1]
- GPU: RTX 4090[^2]
- RAM: 32 GB[^3]
[^1]: https://www.amd.com/de/products/cpu/amd-ryzen-9-5900x
[^2]: https://manli.com/en/product-detail-Manli_GeForce_RTX%C2%AE_4090_Gallardo_(M3530+N675)-312.html
[^3]: https://www.corsair.com/de/de/Kategorien/Produkte/Arbeitsspeicher/VENGEANCE%C2%AE-LPX-32GB-%282-x-16GB%29-DDR4-DRAM-3000MHz-C16-Memory-Kit---Black/p/CMK32GX4M2D3000C16

> "For most of the models generated with the ML-Agents Toolkit, CPU will be faster than GPU." -[Unity documentation](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Unity-Inference-Engine.md)

> "This PPO implementation is not optimized for the use of a GPU. In general, it is not that easy to optimize Reinforcement Learning for the use of a GPU. So you are better of with a CPU currently." -[Marco Pleines, PhD student at TU Dortmund, Deep Reinforcement Learning](https://github.com/Unity-Technologies/ml-agents/issues/1246)

It appears that a powerful CPU is crucial for both training and inference, and contrary to popular belief, the GPU plays a subordinate role in this case, because the implemention of the [reinforcement learning  algorithm "PPO"](https://github.com/yosider/ml-agents-1/blob/master/docs/Training-PPO.md) is supposedly not optimized for GPU usage.[^4][^5]  
To confirm this, the same AI model was trained using both methods. PyTorch in version 1.8.0+cpu (CPU-focused) and 1.8.0+cu111 (GPU-focused) was used. The result was clear in all test runs. The CPU version is capable of performing calculations faster than the GPU Cuda version.  
![pytorch1 8cpuVSgpupng3](https://user-images.githubusercontent.com/37111215/236808865-2cc2bd89-641c-420f-bad5-ef6717e1bd68.png)  
Left side = Training with version 1.8.0+cpu (CPU-focused)  
Right side = Training with version 1.8.0+cu111 (GPU-focused)
[^4]:https://github.com/Unity-Technologies/ml-agents/issues/1246
[^5]:https://github.com/Unity-Technologies/ml-agents/issues/4129

### Customized Training Routines
#### Attempt 1 - Increased Agent Number 
The initial attempt to speed up the training process was to increase the number of agents from 10 to 20. However, this did not yield any performance improvement during testing, and therefore, this method was discarded.

#### Attempt 2 - Concurrent Unity Instances
In the second attempt, [Concurrent Unity Instances](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Training-ML-Agents.md#training-using-concurrent-unity-instances) were used. 
Four environments were created, each with 10 agents undergoing training. Therefore, 40 agents were trained across 4 environments. Since each training session runs at 20 times the normal speed, a speed factor of 800 was achieved. At 10 seconds of real-time training, the AI model was able to train for 133.3 minutes. Using the no-graphics label is useful for saving hardware resources, if there is no need to observe the AI training progress graphically. You can still view a detailed live report of the ongoing training with the [TensorFlow utility named TensorBoard](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Using-Tensorboard.md). The result of this training are in detail stored at this uploaded [TensorBoard](https://tensorboard.dev/experiment/9a0ykmWaRj2aoi56K9X2hA/#scalars). The official definition for the given diagrams can be found [here](https://unity-technologies.github.io/ml-agents/Using-Tensorboard/).
After almost 7 hours of real-time training (equivalent to 5600 hours of AI training), the following main observations were noted:

- High learning progress during the first hour and a half.
- The middle phase was characterized by low learning progress and even a decline in the success progress.
- Towards the end of the training phase, there was a slow but steady improvement in the success performance.

**Value-loss** - Attempt 2 - Concurrent Unity Instances:
![Losses_Value Loss  v20](https://user-images.githubusercontent.com/37111215/236862558-ada2e212-edb4-462a-a57d-de83f3d8d533.svg)  
The value-loss diagram indicates how well the model predicts the values of each state evaluation. During learning, the value-loss diagram should initially increase as the model attempts to improve its prediction capabilities to better understand the environment and make better decisions. Once the model starts to better understand the environment and make better decisions, the value-loss should gradually decrease as the model is able to more accurately predict the state evaluations. In contrast, a consistently increasing value-loss diagram indicates that the model is struggling to understand the environment and make better decisions, leading to poorer performance.  
After completing the training and testing the new AI model, the outcome was disappointing. It did not behave more realistically than the sample AI model provided by the machine learning agents toolkit.

Starting the learning process with concurrent Unity instances:  
`mlagents-learn config/ppo/Walker.yaml --env=C:\Users\username\Desktop\Walker\foldername\UnityEnvironment --num-envs=4 --run-id=MyOwnIdentifier --no-graphics`  

Observing the learning process live via TensorBoard:  
`tensorboard --logdir=C:\Users\username\Documents\GitHub\ml-agents\results\MyOwnIdentifier\Walker`  

Upload the live TensorBoard:  
`tensorboard dev upload --logdir=C:\Users\username\Documents\GitHub\ml-agents\results\MyOwnIdentifier\Walker --name=WalkerDeveloperBranch`

#### Attempt 3 - More Simultaneous Instances and Hidden Units Changes
For this training run, the number of environments was increased to 6. Further tests have shown that under the given conditions (hardware and software versions), a maximum number of 6 concurrent Unity instances is reasonable. Beyond that, no time gain is achieved. At this time during the development phase of this project, a [rare update #5911](https://github.com/Unity-Technologies/ml-agents/pull/5911) (Apr 27, 2023) was released in the developer branch of ML-Agents which affected the walker simulation. Upon installing the new, potentially unstable version and reviewing the revised simulation, a significant improvement in the realism of the sample was observed. 
Several undocumented version conflicts have delayed work on the new version[^16][^18]. Meanwhile, the new sections of code were evaluated and partially integrated into the old version of ML-Agents. After evaluating which new sections should work in the old version, the neural network settings were adopted.  
Before an AI model can be trained, it needs to receive information on how the training will be implemented and executed.
The [training configuration file .yaml](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Training-Configuration-File.md) contains all the relevant information.

[^16]:(https://github.com/Unity-Technologies/ml-agents/issues/5912)
[^18]:(https://github.com/Unity-Technologies/ml-agents/issues/5915)

Excerpt from the .yaml file:  
     
    
    trainer_type: ppo  
    hyperparameters:  
      batch_size: 2048  
      buffer_size: 20480  
      learning_rate: 0.0003  
      beta: 0.005  
      epsilon: 0.2
      lambd: 0.95
      num_epoch: 3
      learning_rate_schedule: linear
    network_settings:
      normalize: true
      hidden_units: 256
      num_layers: 3
      vis_encode_type: simple
    reward_signals:
      extrinsic:
        gamma: 0.995
        strength: 1.0

Reducing the number of hidden units from 512 to 256 represents a significant modification to the AI model, which may lead to faster network training and reduced memory usage, as fewer parameters need to be trained. Additionally, the network may become less susceptible to overfitting, especially if the original network architecture was too large.
On the other hand, decreasing the number of hidden units may result in the network not being able to capture complex relationships in the data as effectively, leading to a poorer model.  

After 75,000,000 steps the training was finished. The time acceleration factor has increased from 800 to 1200 by adding two more instances. Therefore, 10 seconds of training is equivalent to 200 minutes of training for the AI model. The AI model had nearly 1 year (342.5 days) time for training, but just 7 hours and 6 minutes have elapsed.
`(60 agents * 10 seconds_real_time * 20 factor_time_acceleration) / 60 seconds = 200 virtual_minutes_training in 10 real_seconds`
  
In contrast to the previous attempt, there is now a continuous increase in success rate (Environment/Cumulative Reward). Nonetheless, the value-loss diagram (losses/value loss) shows the most significant contrast between the two training runs. The value-loss constantly increased in the previous run but consistently decreased in the current run.  

**Value-loss** - Attempt 3 - More Simultaneous Instances and Hidden Units Changes:
![Losses_Value Loss 256HU](https://user-images.githubusercontent.com/37111215/236862968-f16f6ae2-f3af-405e-98da-3163d59698f9.svg)

The complete training run 3 can be viewed in this [TensorBoard](https://tensorboard.dev/experiment/BB7YBlNnQkqu51mYxkpFDw/#scalars).  
Since no substantial changes were made other than halving the hidden units in the configuration of the neural network, it is now certain that this modification has had a very positive impact on the AI's ability to better understand its environment and make smarter decisions.  
Download this AI model [here](https://drive.google.com/file/d/1tOfK2Jsr-tLzane6bZieoT-tYFFpzJqN/view?usp=sharing).  
Note: This AI model will run on ML-Agents release version 20, but not on the developer branch version.

## Developer Branch Integration
As the brand-new push in the [developer branch](https://github.com/Unity-Technologies/ml-agents/tree/develop) proved to be extremely functional, the project was shifted to it and left the [release-20](https://github.com/Unity-Technologies/ml-agents/tree/release_20) branch. Be aware of version changes mentioned in the [library versions](https://github.com/georghauschild/Human-Motion-AI#library-versions) and exchange required libraries. 
 ```
 Version information: 
  ml-agents: 0.30.0,  
  ml-agents-envs: 0.30.0,  
  Communicator API: 1.5.0,  
  PyTorch: 1.8.0+cpu
```
## Developer Branch AI model
The same training arguments as in the [training attempt 3](https://github.com/georghauschild/Human-Motion-AI#attempt-3---more-simultaneous-instances-and-hidden-units-changes) are being used as they have shown to provide a good improvement in performance:  
`mlagents-learn config/ppo/Walker.yaml --env=C:\Users\username\Desktop\Walker\exe\UnityEnvironment --num-envs=6 --run-id=MyOwnIdentifier --no-graphics`  
The [modified training file](https://github.com/georghauschild/Human-Motion-AI#attempt-3---more-simultaneous-instances-and-hidden-units-changes) is also being taken over from [training attempt 3](https://github.com/georghauschild/Human-Motion-AI#attempt-3---more-simultaneous-instances-and-hidden-units-changes) as it has had a positive impact on the AI's ability to better understand its environment and make smarter decisions through the halving of the hidden units. 

**Cumulative reward** - After even 75 million steps the cumulative reward exhibits a consistently increasing trend over the entire duration of training. There seems to be untapped potential yet to be fully developed.    
![lol](https://user-images.githubusercontent.com/37111215/236912790-41620670-0348-436d-8485-c01f13aeb0c3.svg)

**Value-loss** - Thanks to the advantages by halving the hidden unit value previously identified, the value-loss curve follows a desired decreasing trend.   
![qwe](https://user-images.githubusercontent.com/37111215/236912999-a8e30b12-a5f3-41bb-898a-78ef62156f4d.svg)

**Policy-loss** - It reveals significant outliers at the beginning, but quickly stabilized and assumed a slightly decreasing trajectory.  
![Losses_Policy Loss](https://user-images.githubusercontent.com/37111215/236913530-4247f44d-22dd-4960-849c-531853f3c201.svg)

**Episode-length** - Unlike its predecessors, the episode length of this AI model does not exhibit a significant decrease with increasing training duration, but rather remains relatively stable and gradually increasing.  
![Environment_Episode Length](https://user-images.githubusercontent.com/37111215/236915661-5f688385-179a-499c-a56e-29002c7768ad.svg)
  
The real time training time was 7h and 38min. Multiplied with the performance factor of 1200 it resulted in 381,6 days of virtual training, which is the same as 1,04 years.  
The shown data presented a very positive picture and even suggest untapped potential.

![Unbenanntes Video – Mit Clipchamp erstellt (1)](https://user-images.githubusercontent.com/37111215/236921190-741b0a66-3f8a-4f41-b45f-d9abde8ee052.gif)  
In practical testing, promising results have been observed. The character moves towards its goals in a manner resembling human movement, even alternating the use of both legs. However, its performance could be likened more to a heavily drunk person.

The completed training for the modified developer branch model can be reviewed in this [TensorBoard](https://tensorboard.dev/experiment/0nh8iKjQTKCgABRRbYnz4w/#scalars).  
This AI model can be downloaded [here](https://drive.google.com/file/d/135BaoZc-wM3P2gIU1oGC63QSrbJdEzD0/view?usp=sharing).

## Imitation Learning
GAIL ([Generative Adversarial Imitation Learning](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Training-Configuration-File.md#gail-intrinsic-reward)) is a method used in reinforcement learning to imitate human behavior. It involves training a generator neural network to generate actions that are similar to those taken by a human expert, as determined by a discriminator network that compares the generator's actions to those of the expert. The goal is to minimize the difference between the actions generated by the generator and those of the human expert.

BC stands for [Behavioral Cloning](https://github.com/yosider/ml-agents-1/blob/master/docs/Training-PPO.md#optional-behavioral-cloning-using-demonstrations) and is a machine learning technique used in reinforcement learning. It involves training a model to mimic the behavior of an expert by learning to map observations to actions taken by the expert. The expert's actions are used as training labels, and the model is trained to minimize the difference between its predicted actions and the expert's actions. Once the model is trained, it can be used to make decisions in a new environment.

Both Behavioral Cloning (BC) and Generative Adversarial Imitation Learning (GAIL) are imitation learning methods in reinforcement learning. The main difference between the two is that BC learns a policy by directly imitating an expert's behavior, while GAIL learns from the expert's behavior indirectly by modeling a discriminator network that distinguishes the expert's actions from the policy's actions.

Have a look at the [imitation learning introduction](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/ML-Agents-Overview.md#imitation-learning) by ML-Agents. If not stated otherwise, the reinforcement learning algorithm PPO is always running too.

### GAIL and BC
To activate GAIL and BC the following code has been added to the training configuration file:  
```  
behavioral_cloning:
    demo_path: ./Project/Assets/ML-Agents/Examples/Walker/Demos/ExpertWalker.demo
    strength: 0.5
    steps: 10000  
gail:
    gamma: 0.99
    strength: 0.01
    demo_path: ./Project/Assets/ML-Agents/Examples/Walker/Demos/ExpertWalker.demo
```
During the training process, it was already observed that the computation of the AI model was running very slowly. After 3 hours of training only ~15,000,000 steps were simulated. The previous [developer branch AI model](https://github.com/georghauschild/Human-Motion-AI#developer-branch-ai-model) was able to double the number of simulation runs in the same amount of time. If the additional computations by  imitation learning do not make up for the time loss, this attempt will fail.

**GAIL-loss** - The graph depicts the trajectory of the GAIL loss during the training of a reinforcement learning algorithm. A lower GAIL loss means a better performance of the generator network.  
![Losses_GAIL Loss](https://user-images.githubusercontent.com/37111215/236966687-4fa01c3b-7706-4cfa-82a7-a63c97453ccf.svg)

**Cumulative-reward** - The reward graph, on the other hand, was disappointing. The success increased up to the middle of the training, then decreased and did not return to the previously achieved level at the end.  
![Environment_Cumulative Reward](https://user-images.githubusercontent.com/37111215/236966908-c23b5e95-310d-4d3e-a28a-6f88c38d64df.svg)

As the mixed results of the data suggest, the AI model did not perform convincingly in practical testing. The character walks with an unsteady walk and is prone to falling quickly. The reasons for the failure despite the use of three algorithms need to be investigated. It should not be overlooked, that due to the slow training progress and weak intermediate results, the training was stopped at only ~16.2M steps. The previous AI models got the full 75M simulation cycles.  
Its possible to see all detailed data from both imitation learning algorithm GAIL and BC in this [TensorBoard](https://tensorboard.dev/experiment/qyBzRixwRaiWmDVJ2bEavw/#scalars&runSelectionState=eyIuIjp0cnVlfQ%3D%3D).    
This AI model can be downloaded [here](https://drive.google.com/file/d/19KuNTrYz0ZvWHXRXDdLTl6J26J5R1Bcz/view?usp=sharing).

### BC
Adding this section to the trainer_config enables behavioral cloning for the upcoming training. Increasing the [hyperparameter steps count](https://github.com/yosider/ml-agents-1/blob/master/docs/Training-PPO.md#steps) from 10,000 to 15,000 extends learning through the demonstrator.    
```
behavioral_cloning:
    demo_path: ./Project/Assets/ML-Agents/Examples/Walker/Demos/ExpertWalker.demo
    steps: 150000
    strength: 0.5
```  

The following code shows the standard process, to start the training simulation with 6 instances and no graphical displays, via command. 
This routine was initially developed in the [Customized Training Routines](https://github.com/georghauschild/Human-Motion-AI#customized-training-routines) and has remained unchanged since the [AI model of the developer branch](https://github.com/georghauschild/Human-Motion-AI#developer-branch-ai-model) was created.  
```
(sample-env) PS C:\Users\username\Documents\GitHub\ml-agents> mlagents-learn config/ppo/Walker.yaml --env=C:\Users\username\Desktop\Walker\exe\UnityEnvironment --
num-envs=6 --run-id=JustPPOandBC --no-graphics
```

After 7h 42m 46s the 75M simulation cycles were done. The data looks promising.  
**Cumulative-reward** - Without GAIL, the rewards were stable and consistently achieved.  
![Environment_Cumulative Reward (1)](https://github-production-user-asset-6210df.s3.amazonaws.com/37111215/237121625-c570b5d7-c14b-46a6-bc1a-17546ed2d6ca.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230517%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230517T080405Z&X-Amz-Expires=300&X-Amz-Signature=c433c4c2fce8d2e091a5d4b80365ad5c6c4a67aecd1dd0412441912e19d6b358&X-Amz-SignedHeaders=host&actor_id=37111215&key_id=0&repo_id=636452369)

**Value-loss** - The diagram is also in a good and expected state. [As it should be](https://unity-technologies.github.io/ml-agents/Using-Tensorboard/), it increases while the agent is learning, and then decreases once the reward stabilizes.  
![Losses_Value Loss (1)](https://github-production-user-asset-6210df.s3.amazonaws.com/37111215/237123793-2329a882-c978-49a3-8803-fc668ebb54da.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230517%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230517T080620Z&X-Amz-Expires=300&X-Amz-Signature=c224b36d446e13df724f511a90a36e920b643423f5206a796dcb8679b6bc71b1&X-Amz-SignedHeaders=host&actor_id=37111215&key_id=0&repo_id=636452369)
 
**Pretraining-loss** - Refering how well a model imitates human behavior, this is the only data that exclusively shows successes of behaviour cloning. It measures how close the model comes to the examples it has seen during training. A lower loss number indicates better performance of the model in mimicking the demonstrated behaviors. There is no obvious issue with the use of behaviour cloning here.  
![Losses_Pretraining Loss](https://github-production-user-asset-6210df.s3.amazonaws.com/37111215/237124850-0c5399b8-752e-4b6b-a834-c18c9bea1d2e.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230517%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230517T080627Z&X-Amz-Expires=300&X-Amz-Signature=9d15bfedb9ea7e3c01ecada9e64a228fae7dba3e5b8943f8daa2aa27c2a0f93c&X-Amz-SignedHeaders=host&actor_id=37111215&key_id=0&repo_id=636452369)

The positive results are also reflected in the practical test. The simulation is currently the best and the most realistic.  
![anim1](https://github.com/georghauschild/Human-Motion-AI/assets/37111215/5a3285b8-ece5-4575-b38d-06c6eb547bbe)

The full training for the AI model with only behavioral cloning as imitation learning algorithm can be reviewed in this [TensorBoard](https://tensorboard.dev/experiment/0vJ1A87fQEmRfj3ySpMPYQ/#scalars).  
This AI model can be downloaded [here](https://drive.google.com/file/d/10Ye0oOdzVB7BsZR7QIxBxsgrkaWHXcTf/view?usp=sharing).

## Scripts
In order to achieve human movement and not just getting the agent walking to the target point as fast as possible, following scripts were modified.

### Walker Agent
The target walking speed has been set to 3 to make the agent walk calmly and don't run in haste. An additional benefit of this change is the saving of computational resources, as the AI model now has more time to make decisions. This could potentially reduce training time and save on performance.  
`private float m_TargetWalkingSpeed = 10` -> `private float m_TargetWalkingSpeed = 3`

A dynamically changed walking speed is not required, since the walking speed should be at level 3.  
`public bool randomizeWalkSpeedEachEpisode` -> `public bool randomizeWalkSpeedEachEpisode = false`

These changes were made in the file WalkerAgent.cs.

### Decision Requester
The decision period refers to the time interval during which the agent makes its decisions. During the decision period, the agent analyzes the current state of the environment, performs calculations to choose a suitable action, and then executes that action. This process is repeated in every decision period, until the training is completed or the end of the game is reached.  
The decision period has been reduced to 1, because the slower speed of the character means that fewer decisions need to be made in a short period of time. In brief tests, the AI model did not exhibit any negative abnormalities, although it now only has 20% of the decision frequency.  
`public int DecisionPeriod = 5` -> `public int DecisionPeriod = 1`

When the following option is enabled, the character performs actions between decision periods. This can be useful to make the agent respond more quickly to its environment or to ensure that the agent stays in motion during training. Its important to note that executing actions between decisions can come with additional costs, as the AI model requires more computation time to execute the actions and may require more resources to update the environment. Therefore, this option should be carefully weighed to ensure that the benefit it provides justifies the additional costs.  
`public bool TakeActionsBetweenDecisions = true` -> `public bool TakeActionsBetweenDecisions = false`

These changes were made in the file DecisionRequester.cs.

### Demonstration File
The effectiveness of BC depends on the quality of the demonstration file. The sample demonstration file has the following values:  
![g1](https://github.com/georghauschild/Human-Motion-AI/assets/37111215/8c9ca216-eb0b-4f75-aed9-ba8cb4e858d7)

Recording a new demo file resulted in following values:  
![g2](https://github.com/georghauschild/Human-Motion-AI/assets/37111215/ce07ecbb-b50b-4660-b55a-35a15c9c1046)

### Generate AI Model
To use the modified scripts for training, it is necessary to rebuild the Unity application, especially when using [concurrent Unity instances](https://github.com/georghauschild/AIreadmeTest#attempt-2---concurrent-unity-instances).  
The duration of behavioral cloning was increased.
``
behavioral_cloning:
     demo_path: ./Project/Assets/ML-Agents/Examples/Walker/Demos/demo1617.demo
     strength: 0.5
     steps: 250000
``
For the 75M runs that have been set up so far, this run took a total of 15 hours and 40 minutes. 
This is approximately twice the time consumption compared the last run, which also had only BC as addtional algorithm. 
Apparently, contrary to expectations, the script modifications did not lead to an acceleration, but rather to a doubling of the training time.

**Cumulative-reward** - The reward graph depicts a steady and above-average increase until the end of the training.  
![Environment_Cumulative Reward (2)](https://github-production-user-asset-6210df.s3.amazonaws.com/37111215/237466509-3cdb638f-d576-49c1-adee-c41810821751.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230517%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230517T081729Z&X-Amz-Expires=300&X-Amz-Signature=b1b1b9a0121310af6e267013b9153d04840d6a3f9b3347772ea57ba65ab3f6b5&X-Amz-SignedHeaders=host&actor_id=37111215&key_id=0&repo_id=636452369)

**Value-loss** - The graph is highly pronounced, showing a sharp increase during learning and a steep drop after the reward stabilizes.
![Losses_Value Loss (2)](https://github-production-user-asset-6210df.s3.amazonaws.com/37111215/237467182-9cfa7712-4d4d-4275-ae8b-b5a6b8ef1245.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230517%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230517T081746Z&X-Amz-Expires=300&X-Amz-Signature=adbd6cace0faf224bddbaaad8ec2555db8333e353e044ce70e03ab2f84a2caed&X-Amz-SignedHeaders=host&actor_id=37111215&key_id=0&repo_id=636452369)


The exceptionally long training has produced satisfactory data training results. The practical experiment, however, did not live up to expectations. The body posture and frequent falling are unsatisfactory. On the other hand, the leg work seems to be well-developed and stands out positively. The overall performance appears somewhat jittery.  
![anim2](https://github.com/georghauschild/Human-Motion-AI/assets/37111215/61e383b2-fbda-4775-9410-a3a06401ba08)


The full training can be reviewed in this [TensorBoard](https://tensorboard.dev/experiment/sDUEKnKMQf66Q6mUW0m9hw/#scalars).  
This AI model can be downloaded [here](https://drive.google.com/file/d/1XRRGducTOTjckDv7NT8p6kmU7oRVnK8_/view?usp=sharing).

## Installation and Version Instructions
It is important to proceed with caution during installation, especially when dealing with the developer version, as it is easy to encounter version conflicts.

### Installation Advice
Follow the [official installation instructions](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md). 

Make sure to open the main directory of ML Agents in the terminal or select the directory afterwards.  
- example: `PS C:\Users\username\Documents\GitHub\ml-agents>`

Creating and using a [virtual environment](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Using-Virtual-Environment.md) has proven to be useful. It prevents version conflicts and will potentially save a significant amount of time.  
- create a new environment: `python -m venv python-envs\sample-env`  
- activate the environment: `python-envs\sample-env\Scripts\activate`

### Library Versions
A well-known and time-consuming issue is getting the framework to run, especially for training purposes. The following versions of the libraries work seamlessly together.  
The Release 20 is the latest current stable version. The developer branch is experimental, may provide more features, but can be unstable and may cause version conflicts.

Release version [20](https://github.com/Unity-Technologies/ml-agents/tree/release_20):
- Machine learning agents toolkit release version 20[^6]
- Unity 2021.3.14f1[^7]
- Python 3.9.12[^8]
- Ml-agents python package 0.30[^9]
- pip 23.1.2[^10]
- PyTorch 1.7.1+cu110[^11]
- Ml-agents Unity package 2.0.1[^12]
- Tensorboard 2.13.0[^13]
- Windows 11[^14]
- Numpy 1.21.2[^15]

Developer Branch up to [#5911](https://github.com/Unity-Technologies/ml-agents/pull/5911):  
- Machine learning agents toolkit developer branch #5911[^19]
- Unity 2021.3.14f1[^7]
- Python 3.9.12[^8]
- Ml-agents python package 0.30[^9]
- pip 23.1.2[^10]
- PyTorch 1.8.0+cpu[^17]
- Ml-agents Unity package 2.3.0-exp.4<sup>display error? it's maybe exp.3</sup>
- Tensorboard 2.13.0[^13]
- Windows 11[^14]
- Numpy 1.21.2[^15]
- Protobuf 3.20[^20]

*It may also work for future iterations of the developer branch, but it has only been tested up to [#5911](https://github.com/Unity-Technologies/ml-agents/pull/5911).*


[^6]:https://github.com/Unity-Technologies/ml-agents/tree/release_20
[^7]:https://unity.com/releases/editor/whats-new/2021.3.14
[^8]:https://www.python.org/downloads/release/python-3912/
[^9]:https://libraries.io/pypi/mlagents
[^10]:https://pypi.org/project/pip/
[^11]:https://pytorch.org/get-started/previous-versions/
[^12]:https://docs.unity.cn/Packages/com.unity.ml-agents@2.0/changelog/CHANGELOG.html#201---2021-10-13
[^13]:https://github.com/tensorflow/tensorboard/releases/tag/2.13.0
[^14]:https://www.microsoft.com/de-de/software-download/windows11
[^15]:https://numpy.org/doc/stable/release/1.21.2-notes.html
[^17]:https://pytorch.org/get-started/previous-versions/
[^18]:https://docs.unity3d.com/Packages/com.unity.ml-agents@2.3/manual/index.html
[^19]:https://github.com/Unity-Technologies/ml-agents/pull/5911
[^20]:https://github.com/protocolbuffers/protobuf/tree/3.2.x
