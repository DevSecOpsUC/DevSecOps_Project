# 🛡️ DevSecOpsUC — Manual Práctico de Buenas Prácticas DevSecOps

> **Universidad Católica de Colombia**  
> Facultad de Ingeniería — Programa de Ingeniería de Sistemas  
> **Proyecto Académico: Implementación de un Entorno DevSecOps Completo**

---

![Build Status](https://img.shields.io/github/actions/workflow/status/FelipeBarrios/DevSecOps_Project/sonarcloud.yml?label=Build%20Status&logo=github&color=brightgreen)
![SonarCloud](https://img.shields.io/sonar/quality_gate/FelipeBarrios_DevSecOps_Project?server=https%3A%2F%2Fsonarcloud.io&logo=sonarcloud&color=orange)
![CodeQL](https://img.shields.io/github/actions/workflow/status/FelipeBarrios/DevSecOps_Project/codeql-analysis.yml?label=CodeQL%20SAST&logo=github&color=blue)
![DockerHub](https://img.shields.io/badge/Docker%20Images-Automated%20Builds-blue?logo=docker)
![Azure Deploy](https://img.shields.io/badge/Deployed%20on-Azure%20Web%20App-blue?logo=microsoftazure)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📘 Descripción General

El proyecto **DevSecOpsUC** integra desarrollo, seguridad y operaciones dentro de un flujo automatizado de **Integración y Entrega Continua (CI/CD)** utilizando herramientas modernas de la nube.  
Forma parte del **Manual de Buenas Prácticas DevSecOps**, que busca acercar a los estudiantes a la cultura DevSecOps mediante experiencias prácticas, desde la **codificación segura** hasta la **gobernanza en Azure**.

---

## 🧩 Estructura General del Proyecto

```bash
DevSecOps_Project/
├── Unidad1_AsegurandoCodigoFuente/
│   ├── workflows/
│   ├── CodeQL/
│   ├── SonarCloud/
│   └── docs/
│
├── Unidad2_DespliegueSeguro/
│   ├── frontend-web/
│   ├── auth-service/
│   ├── rooms-service/
│   ├── docker-compose.yml
│   └── workflows/
│
├── Unidad3_GobernanzaYCumplimiento/
│   ├── azure-policy/
│   ├── sentinel/
│   ├── compliance/
│   └── docs/
│
└── .github/
    └── workflows/
```

## ⚙️ Tecnologías y Herramientas Utilizadas

| **Categoría** | **Herramienta / Servicio** | **Descripción** |
|----------------|----------------------------|-----------------|
| **Repositorio y CI/CD** | GitHub Actions | Orquestación de pipelines automatizados |
| **Análisis Estático (SAST)** | CodeQL / SonarCloud | Evaluación de vulnerabilidades y calidad de código |
| **Análisis Dinámico (DAST)** | OWASP ZAP *(en desarrollo)* | Escaneo de vulnerabilidades en aplicaciones desplegadas |
| **Contenedores** | Docker / DockerHub / Azure Container Registry | Empaquetado, pruebas y despliegue |
| **Infraestructura en la nube** | Microsoft Azure | Entorno principal para pruebas, despliegue y monitoreo |
| **Gobernanza y Seguridad** | Azure Policy / Microsoft Sentinel / Defender | Supervisión, políticas de cumplimiento y alertas |

---

## 🚀 Flujo CI/CD Implementado

El ciclo automatizado de integración continua consta de tres fases principales:

### 1️⃣ Aseguramiento del Código Fuente
- Implementación de **CodeQL** y **SonarCloud** para análisis estático.  
- Los secretos y tokens se obtienen dinámicamente desde **Azure Key Vault**.  
- Validación de vulnerabilidades **OWASP Top 10** en el código .NET.  

### 2️⃣ Despliegue Seguro
- Construcción automática de imágenes **Docker**.  
- Publicación en **DockerHub** y/o **Azure Container Registry (ACR)**.  
- Despliegue automatizado mediante **Azure Web App for Containers**.  

### 3️⃣ Gobernanza y Cumplimiento
- Monitoreo continuo con **Microsoft Sentinel**.  
- Evaluación de políticas con **Azure Policy**.  
- Cumplimiento de estándares de seguridad del **Cloud Adoption Framework (CAF)**.  

---

## 🧠 Unidades del Manual Práctico

| **Unidad** | **Título** | **Enfoque** |
|-------------|-------------|-------------|
| **Unidad 1** | Asegurando el Código Fuente | Aplicación de principios OWASP y codificación defensiva |
| **Unidad 2** | Despliegue Seguro | Construcción, contenedorización y despliegue automatizado en Azure |
| **Unidad 3** | Gobernanza y Cumplimiento | Seguridad en la nube y monitoreo con políticas y Sentinel |

---

## 🧾 Requisitos Previos

- Cuenta **Azure for Students** o suscripción activa.  
- **GitHub** con Actions habilitadas.  
- **Docker Desktop** instalado localmente.  
- **Visual Studio / VS Code** con SDK .NET 8.0.  
- **Azure CLI** configurado con credenciales.  
- Cuentas activas en **SonarCloud** y **DockerHub**.  

---

## 🧩 Arquitectura General del Proyecto

El ecosistema de la aplicación está compuesto por microservicios:

- **frontend-web:** interfaz principal de usuario.  
- **auth-service:** autenticación de usuarios y tokens JWT.  
- **rooms-service:** gestión de habitaciones y datos simulados.  

Integración CI/CD mediante **workflows YAML** que gestionan análisis, construcción y despliegue.  

---

## 📊 Integraciones con Seguridad en la Nube (Azure)

- **Azure Policy:** control de cumplimiento de recursos y configuraciones.  
- **Microsoft Defender for Cloud:** evaluación de seguridad y recomendaciones.  
- **Microsoft Sentinel:** reglas analíticas para detección de amenazas en logs.  
- **AppServiceLogs:** registro centralizado de eventos de aplicación.  

---

## 🧑‍💻 Autores

| **Nombre** | **Rol** | **Institución** |
|-------------|----------|-----------------|
| **Felipe Barrios** | Autor Principal – Implementación Técnica y Documentación | Universidad Católica de Colombia |
| **Allison López** | Autor Principal – Implementación Técnica y Documentación | Universidad Católica de Colombia |

© **2025 – Universidad Católica de Colombia**  
**Manual de Buenas Prácticas DevSecOps — Proyecto Académico**
