# SimpleAcsCallBot
![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)  
![Azure App Service](https://img.shields.io/badge/Azure-App%20Service-0089D6.svg)  
![Azure Communication Services](https://img.shields.io/badge/Azure-Communication%20Services-0078D4.svg)  
![Azure Event Grid](https://img.shields.io/badge/Azure-Event%20Grid-5C2D91.svg)  

---

## Overview
**SimpleAcsCallBot** is an ASP.NET Core Web API that integrates **Microsoft Teams Phone System**, **Azure Communication Services (ACS)**, and **Azure Event Grid** to automatically answer incoming calls and play an audio message hosted on **Azure Blob Storage**.

This project demonstrates:
- Receiving incoming calls via **Teams Phone Extensibility**.
- Handling `IncomingCall` events from **Event Grid**.
- Using **ACS Call Automation SDK** to answer calls and play audio.

---

## Architecture

Teams Resource Account → ACS → Event Grid → AcsCallBot (Azure App Service) → Audio from Blob Storage

---

## Features
- Handles **IncomingCall** events from ACS via Event Grid.
- Answers calls using **ACS Call Automation SDK**.
- Plays audio from **Azure Blob Storage**.
- Uses **Azure App Service environment variables** for configuration.

---

## Prerequisites
- Azure Subscription.
- **Azure Communication Services** resource.
- **Teams Resource Account** with phone number and license.
- **Azure Blob Storage** with an audio file (WAV recommended).
- **Azure App Service** for hosting the Web App.
- **Event Grid** subscription for ACS events.

---

## Configuration
Set these environment variables in **Azure App Service → Configuration → Environment variables**:

| Key                   | Description                                      |
|-----------------------|--------------------------------------------------|
| `ACS_CONNECTION_STRING` | Connection string of your ACS resource.         |
| `AUDIO_URL`            | Public or SAS URL of the audio file in Blob Storage. |
| `APP_URL`              | Public URL of your App Service (e.g., `https://<app>.azurewebsites.net`). |

---

## Endpoints
- `POST /incomingCall`  
  Handles:
  - **Event Grid validation** (`SubscriptionValidationEvent`).
  - **IncomingCall** event: answers the call and plays the audio file.

---

## Download
You can download the latest ready-to-use package here:

[![Download ZIP](https://img.shields.io/badge/Download-ZIP-blue)](https://github.com/LucaVitali/SimpleAcsCallBot/latest

---

## Deployment
1. Zip the project and deploy via **Azure App Service → Deployment Center → Zip Deploy**.
2. Configure environment variables.
3. Create an **Event Grid subscription**:
   - Event Type: `IncomingCall`.
   - Endpoint: `https://<your-app>.azurewebsites.net/incomingCall`.

---

## Testing
- Call the Teams number associated with the Resource Account.
- Check logs in **App Service → Log Stream** for:
  - Validation success.
  - IncomingCallContext.
  - Audio playback confirmation.

---

## Future Enhancements
- Add **Text-to-Speech (TTS)** using Azure Cognitive Services.
- Implement **DTMF input handling** for IVR menus.
- Integrate with **Copilot Studio** for conversational logic.

---

## License
MIT License
