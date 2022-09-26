# Tic Tac Toe TDD
Tic Tac Toe implementation using TDD and made all tests run on every new push and pull request using [Unity Actions](https://github.com/game-ci/unity-actions).

<p align="center">
  <a>
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <img alt="License" src="https://img.shields.io/github/license/JoanStinson/TicTacToeTDD?logo=github">
  </a>
  <a>
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/JoanStinson/TicTacToeTDD?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/JoanStinson/TicTacToeTDD?logo=VirtualBox">
  </a>
  <a>
    <img alt="Downloads" src="https://img.shields.io/github/downloads/JoanStinson/TicTacToeTDD/total?color=brightgreen">
  </a>
  <a>
    <img alt="Last Release" src="https://img.shields.io/github/v/release/JoanStinson/TicTacToeTDD?include_prereleases&logo=Dropbox&color=yellow">
  </a>
</p>

<p align="center">
  <a>
    <img alt="Build" src="https://github.com/JoanStinson/TicTacToeTDD/workflows/Build/badge.svg">
  </a>
  <a>
    <img alt="Unity Code Coverage" src="https://github.com/JoanStinson/TicTacToeTDD/blob/main/CodeCoverage/Report/badge_linecoverage.svg">
  </a>
  <a>
    <img alt="Sonar Cloud Code Coverage" src="https://sonarcloud.io/api/project_badges/measure?project=DanielParra159_HangmanGame&metric=coverage">
  </a>
  <a>
    <img alt="Maintainability Rating" src="https://sonarcloud.io/api/project_badges/measure?project=DanielParra159_HangmanGame&metric=sqale_rating">
  </a>
  <a>
    <img alt="Quality Gate" src="https://sonarcloud.io/api/project_badges/measure?project=DanielParra159_HangmanGame&metric=alert_status">
  </a>
  <a>
    <img alt="Technical Debt" src="https://sonarcloud.io/api/project_badges/measure?project=DanielParra159_HangmanGame&metric=sqale_index">
  </a>
</p>

## 📜 Kata Rules
* A game has nine fields in a 3x3 grid.
* There are two players in the game (X and O).
* A player can take a field if not already taken.
* Players take turns taking fields until the game is over.
* A game is over when all fields in a row are taken by a player.
* A game is over when all fields in a column are taken by a player.
* A game is over when all fields in a diagonal are taken by a player.
* A game is over when all fields are taken.

## 🧩 Continuous Integration
I have created a continuous integration workflow via [GitHub Actions](https://github.com/features/actions), more specifically via [Unity Actions](https://github.com/game-ci/unity-actions). Every time a new push is commited to the repository or a new pull request is created, all edit mode and play mode tests run.
<p align="center">
  <img width="824" height="283" src="https://github.com/JoanStinson/TicTacToeTDD/blob/main/ci workflow.PNG">
</p>

## 🔍 Code Analysis
I have used the [Unity Code Coverage](https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@0.2/manual/index.html) package to generate reports of both edit and play mode tests, like the one in the following image. This is super helpful to feel safe about your code. The more coverage, the more robust! 💪
<p align="center">
  <img width="1040" height="462.5" src="https://github.com/JoanStinson/TicTacToeTDD/blob/main/coverage.PNG">
</p>
