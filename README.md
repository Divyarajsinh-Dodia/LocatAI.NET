# 🔍 LocatAI

<div align="center">
  
  ![AI Powered](https://img.shields.io/badge/AI%20Powered-GPT--4-blue?style=for-the-badge&logo=openai)
  ![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
  ![Selenium](https://img.shields.io/badge/Selenium-43B02A?style=for-the-badge&logo=selenium&logoColor=white)
  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

  <h3>🤖 AI-powered element location for Selenium tests</h3>
  <p>Find any element using natural language - no more brittle locators!</p>

  <p>
    <a href="CHANGELOG.md"><strong>View Changelog »</strong></a> •
    <a href="#-how-to-use"><strong>Get Started »</strong></a>
  </p>
</div>

---

## 📋 Table of Contents
- [🚀 How to Use](#-how-to-use)
  - [📋 Prerequisites](#-prerequisites)
  - [📦 Installation](#-installation)
  - [🔑 Configuration](#-configuration)
  - [▶️ Getting Started](#%EF%B8%8F-getting-started)
- [💡 What Makes LocatAI Unique](#-what-makes-locatai-unique)
  - [🧠 Smart Element Finding](#-smart-element-finding)
  - [💾 Efficient Caching](#-efficient-caching)
  - [👆 DOM Fingerprinting](#-dom-fingerprinting)
  - [⏱️ Smart Timeouts](#%EF%B8%8F-smart-timeouts)
  - [🔒 Data Protection](#-data-protection)
  - [📏 DOM Size Optimization](#-dom-size-optimization)
  - [📊 Diagnostics & Reporting](#-diagnostics--reporting)
  - [🛡️ Reliability Features](#%EF%B8%8F-reliability-features)
- [⚙️ How LocatAI's Caching System Works](#%EF%B8%8F-how-locatais-caching-system-works)
- [🔮 Roadmap](#-roadmap)

---

## 🚀 How to Use

### 📋 Prerequisites

To use LocatAI.NET, you need:
- .NET 6.0 or higher
- Selenium WebDriver for .NET
- An OpenAI API key
- Basic knowledge of Selenium WebDriver

> **⚠️ Note:** Currently, LocatAI.NET works exclusively with OpenAI's models only. Support for other models is coming in future updates.

### 📦 Installation

Install via NuGet Package Manager:

```powershell
Install-Package LocatAI.NET
```

Or via .NET CLI:

```bash
dotnet add package LocatAI.NET
```

### 🔑 Configuration

Create or edit the `appsettings.json` in your project:

```json
{
  "LocatAI": {
    "OpenAIApiKey": "your-api-key",
    "OpenAIModel": "gpt-4",
    "DefaultWaitTimeoutSeconds": 10,
    "PerformanceThresholdMultiplier": 1.5,
    "FindElementRetryAttempts": 2,
    "DomSampleSizeBytes": 5000,
    "DomTruncateTextLength": 1000,
    "Cache": {
      "CacheDirectory": "../../../Cache",
      "Enabled": true,
      "MaxAgeHours": 24,
      "MaxSize": 100,
      "ExpirySeconds": 3600,
      "CacheFileName": "locator_cache.json",
      "StatsFileName": "locator_stats.json",
      "TimingFileName": "locator_timing.json",
      "MinAttemptsForEviction": 5,
      "MinSuccessRateForEviction": 0.6,
      "MaxFailuresForEviction": 3
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

### ▶️ Getting Started

Use LocatAI.NET in your Selenium tests:

```csharp
using LocatAI.NET;

// Initialize WebDriver with LocatAI
var driver = new ChromeDriver();

// Find elements using natural language
var loginButton = await driver.FindElementByAIAsync("the login button");
var emailField = await driver.FindElementByAIAsync("email input field");

// Regular Selenium actions
emailField.SendKeys("test@example.com");
loginButton.Click();
```

---

## 💡 What Makes LocatAI Unique

### 🧠 Smart Element Finding

<div style="padding: 10px; background-color: #f0f5ff; border-radius: 5px; border-left: 5px solid #0066ff; margin-bottom: 20px;">
LocatAI uses natural language processing to find elements on a webpage. Simply describe what you're looking for in plain English, and LocatAI will handle the translation to appropriate Selenium locators.
</div>

```csharp
var usernameField = await driver.FindElementByAIAsync("username input field");
```

### 💾 Efficient Caching

LocatAI implements an intelligent caching system that stores successful element locators, reducing redundant API calls and speeding up test execution for repeated element interactions.

<details>
<summary><b>📝 View Example Cache Structure</b></summary>

Example of how locators are cached in the `locator_cache.json` file:

```json
{
  "https://www.saucedemo.com/v1/::username input field::3bc231965b": [
    "ID", 
    "user-name", 
    1744291719.5895622
  ],
  "https://www.saucedemo.com/v1/::password input field::3bc231965b": [
    "ID", 
    "password", 
    1744291719.6506157
  ],
  "https://www.saucedemo.com/v1/::login button::3bc231965b": [
    "ID", 
    "login-button", 
    1744291719.7035625
  ]
}
```
</details>

The cache key format is: `URL::natural language description::DOM fingerprint`. Each entry contains:
1. The locator strategy (ID, CSS_SELECTOR, XPATH, etc.)
2. The actual locator value
3. A timestamp for cache management

### 👆 DOM Fingerprinting

The system creates unique DOM fingerprints to ensure cache invalidation when page content changes significantly, maintaining reliability while maximizing cache hits.

### ⏱️ Smart Timeouts

Unlike static timeout settings, LocatAI automatically adjusts wait times based on historical element load times, making tests more efficient and robust.

<details>
<summary><b>📝 View Example Timing Data</b></summary>

Example of timing data from `locator_timing.json`:

```json
{
  "https://www.saucedemo.com/v1/::username input field": {
    "count": 11,
    "total": 3.0696456432,
    "avg": 0.2790586948
  },
  "https://www.saucedemo.com/v1/::password input field": {
    "count": 11,
    "total": 4.1260349750, 
    "avg": 0.3750940886
  }
}
```
</details>

### 🔒 Data Protection

Before sending DOM content to external APIs, LocatAI automatically identifies and removes sensitive data such as passwords, tokens, and other confidential information.

### 📏 DOM Size Optimization

LocatAI reduces DOM size by removing unnecessary script content and other elements that aren't needed for element location, improving performance and reducing API costs.

### 📊 Diagnostics & Reporting

Get detailed usage statistics:

```csharp
var report = AIUsageTracker.Instance.GetReport();
Console.WriteLine($"API Success Rate: {report.ApiSuccessRate:P2}");
Console.WriteLine($"Cache Hit Rate: {report.CacheHitRate:P2}");
Console.WriteLine($"Estimated Cost: ${report.EstimatedCost:F2}");
```

### 🛡️ Reliability Features

LocatAI implements several features to ensure reliable test execution:
- 📈 Tracks element locator success rates over time
- 🚫 Automatically removes unreliable locators from cache
- 👆 Uses DOM-aware fingerprinting for accurate cache hits
- 🔄 Implements intelligent retry mechanisms with adaptive waits

---

## ⚙️ How LocatAI's Caching System Works

The caching system consists of three interconnected JSON files that work together to improve performance and reliability:

1. **`locator_cache.json`** - Stores successful element locators
2. **`locator_stats.json`** - Maintains success/failure statistics
3. **`locator_timing.json`** - Records timing data for smart timeouts

### 💰 Cost Optimization

| Phase | API Calls | Cost Impact |
|-------|-----------|-------------|
| Initial Run | Higher (Learning) | Standard API costs |
| Subsequent Runs | 60-90% reduction | Significant savings |
| Maintenance | As needed | Minimal costs |

---

## 🔮 Roadmap

Upcoming features for LocatAI.NET:

| Coming Soon | Description |
|-------------|-------------|
| 🤖 **Gemini Support** | Integration with Google's Gemini models |
| 🧠 **Claude Support** | Integration with Anthropic's Claude |
| 💻 **Local Models** | Support for running with local LLMs |
| 🌐 **Cross-Browser** | Enhanced cross-browser compatibility |
| 📱 **Mobile Testing** | Support for Appium integration |

<div style="padding: 10px; background-color: #fff0f0; border-radius: 5px; border-left: 5px solid #ff6666; margin-top: 20px;">
Please report any issues or feature requests on our GitHub repository.
</div>
