Hyper Casual Card Sorting Game

A hyper-casual mobile game built in Unity inspired by Card Factory!.
The game focuses on satisfying conveyor-based mechanics where players tap card crates, move cards along a conveyor system, and match them with the correct destination crates.

The project demonstrates clean architecture, modular systems, and smooth gameplay animations using modern Unity tools and third-party plugins.

Gameplay Overview

The core gameplay revolves around sorting colored cards using a conveyor system.

Players interact with crates containing cards of specific colors and send them through a conveyor path where they automatically travel toward matching destination crates.

Core Gameplay Mechanics

The player taps on a crate containing cards of a single color.

Cards are released and move onto the conveyor path.

Cards travel forward smoothly using Dreamteck Splines.

When cards reach their matching color crate, they jump into the accepting crate.

Once a crate reaches its capacity, it disappears using a smooth DOTween animation.

The system continues dynamically as new crates spawn and the layout adjusts.

Technical Implementation

The project focuses on smooth gameplay systems and scalable architecture.

Input System

Tap-based interaction implemented using raycasting.

Card Sorting Logic

Color matching handled through card and crate color validation logic.

Animations

Smooth jump, rotation, and scale animations implemented using DOTween.

Conveyor Movement

Cards move along a spline path using Dreamteck SplineFollower.

Spline System

Conveyor paths are generated using Dreamteck spline mesh tools.

Additional Improvements Beyond Requirements

To make the project more scalable and production-ready, several improvements were implemented:

Runtime spawning of both card crates and accepting crates.

ScriptableObject-based libraries used to manage card types and accepting crate configurations.

Deterministic spawning logic ensures matching numbers and colors between card crates and accepting crates.

Dynamic crate shifting system automatically reorganizes crates when one disappears.

Modular and scalable architecture with clear separation of responsibilities between systems.

Tech Stack

Engine: Unity

Language: C#

Animation: DOTween

Spline System: Dreamteck Splines

Architecture: Modular system with ScriptableObjects

Project Goals

This project was created to demonstrate:

Hyper-casual gameplay design

Modular Unity architecture

Third-party tool integration

Smooth animation systems

Scalable gameplay systems

YouTube Video Link:
https://www.youtube.com/watch?v=-fKC1-M1PeA

Author
Aryan Jadhav
Unity Developer
