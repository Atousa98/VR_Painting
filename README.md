# VirtuArt: GPU RenderTexture VR Painting Prototype

[![Unity](https://img.shields.io/badge/Unity-URP-blue.svg)](https://unity.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Research: Master Thesis](https://img.shields.io/badge/Research-Master%20Thesis-purple.svg)]()

> **Core Prototype for Master's Thesis:**  
> *VirtuArt: Exploring Art Appreciation Through Embodied Difficulty and Physical Friction in Virtual Reality*  
> Master's Programme in Interactive Media, University of Applied Sciences Upper Austria (FH Hagenberg).

---
## 🎬 System Demonstration

[![Watch Demo Video](https://img.shields.io/badge/GitHub_Release-Watch_Demo_Video-blue?style=for-the-badge&logo=github)](https://github.com/atousasque/My_Portfolio/releases/download/v1.10.0/Atousa_Garousian.mp4)

> 🎥 **Interactive Gameplay & Prototype Walkthrough:**  
> [Click here to view/download the high-resolution prototype demonstration](https://github.com/atousasque/My_Portfolio/releases/download/v1.10.0/Atousa_Garousian.mp4) showcasing pigment drag, dynamic proximity scaling, and contact haptics.
>
## Overview

**VirtuArt** investigates how intentionally preserved interaction friction and embodied physical constraints in Virtual Reality can bridge the gap in appreciating minimalist and traditional fine art. Unlike mainstream commercial VR creative applications (such as Tilt Brush or Open Brush) that prioritize algorithmic smoothing, frictionless undo mechanisms, and geometric stabilization, VirtuArt is engineered as an embodied perceptual instrument.

This repository hosts the **RenderTexture (GPU Camera-Capture)** prototype. In this implementation, digital mark-making preserves genuine human motor constraints—including hand tremors, sustained arm fatigue, dynamic line weight scaling, and tactile surface feedback—capturing the irreversible, physical commitment inherent to traditional oil painting.

---

## System Architecture & Technical Highlights

* **GPU RenderTexture & Camera Projection:**  
  A dedicated orthographic camera captures 3D brush interactions and projects continuous strokes onto a high-resolution, dynamic `RenderTexture` (1024×1024) mapped across the canvas UV space.
* **Material & Color Blending Shaders:**  
  Custom URP fragment shaders handle layered alpha blending and color accumulation, mimicking the organic drag and semi-transparent viscosity of oil paint rather than hard pixel replacement.
* **Proximity-Based Dynamic Scaling:**  
  Brush line thickness and visual weight dynamically adapt based on physical spatial proximity along the Z-axis, enforcing deliberate fine-motor arm control.
* **Spatio-Temporal Contact Haptics:**  
  Controller vibrotactile feedback triggers precisely upon physical brush-tip collision with the canvas surface plane, establishing an authentic tactile confirmation of mark initiation.
* **Sustained Grip & Fatigue Modeling:**  
  Requires continuous physical grip force to maintain brush engagement, mirroring the muscular exertion and mechanical resistance of manual tool handling.

---

## Evaluated in Academic Research

This prototype served as **Prototype 1** in our two-phase research methodology:
1. **Formative Expert Evaluation ($N=4$):** Evaluated against a CPU Raycast baseline by a traditional master painter, an HCI professor, a graphics programmer, and a digital artist. The RenderTexture architecture was selected for its superior pigment-drag realism, sensorimotor feedback, and GPU pipeline scalability.
2. **Summative User Study ($N=20$):** Empirically tested across a $2 \times 2$ factorial matrix (varying prior art and VR familiarity), demonstrating that friction-preserving VR interaction significantly shifts participants' appreciation and understanding of the technical discipline behind minimalist paintings.

---

## 🚀 Getting Started

### Prerequisites
* **Unity Version:** 2022.3 LTS (or recommended URP-supported version)
* **Render Pipeline:** Universal Render Pipeline (URP)
* **VR SDK:** XR Interaction Toolkit / OpenXR
* **Target Hardware:** Meta Quest 2 / Quest 3 / Quest Pro (via PCVR / Oculus Link)

### Installation
1. Clone this repository:
   ```bash
   git clone [https://github.com/Atousa98/VR_Painting.git](https://github.com/Atousa98/VR_Painting.git)
