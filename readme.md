[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
[![openupm](https://img.shields.io/npm/v/com.hgs.hgs-unity-float-point-system?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.hgs.hgs-unity-float-point-system/)

# Introduction
The **HGS Float Point** implements a `TextMeshPro` pool to show feedbacks in your scene.

# Usage

1) Click in `+` button in `Project` window, and go to `HGS > FloatPoint > Asset`.
![](/.github/images/create.png)

2) Configure the `FloatPointAsset` as your like.
![](/.github/images/scriptable.png)

3) Refer `FloatPointAsset` created, in you code like this:

```cs
using HGS.FloatPoint

public class Unit: MonoBehaviour
{
  [SerializeField] FloatPointAsset floatPoint;
  [SerializeField] int health = 100;
  [SerializeField] Color criticalColor;
  [SerializeField] Color healColor;
  [SerializeField] Color damageColor;

  private void Spawn()
  {
    var value = UnityEngine.Random.Range(-200, 100);

    if (value < -100)
    {
      floatPoint.Show($"<sprite=1 tint=1> {Mathf.Abs(value)}", transform, criticalColor, 4);
    }
    else if (value < 0)
    {
      floatPoint.Show($"<sprite=0 tint=1> {Mathf.Abs(value)}", transform, damageColor, 3);
    }
    else
    {
      floatPoint.Show($"<sprite=2 tint=1> {Mathf.Abs(value)}", transform, healColor, 3);
    }
  }
}
```

4) Result

![](/.github/images/showcase.gif)

## Installation

OpenUPM:

`openupm add com.hgs.hgs-unity-float-point-system`

Package Manager:

`https://github.com/homy-game-studio/hgs-unity-hgs-unity-float-point-system.git#upm`

Or specify version:

`https://github.com/homy-game-studio/hgs-unity-hgs-unity-float-point-system.git#1.0.0`

# Samples

You can see all samples directly in **Package Manager** window.

# Contrib

If you found any bugs, have any suggestions or questions, please create an issue on github. If you want to contribute code, fork the project and follow the best practices below, and make a pull request.

## Namespace Convention

To avoid script collisions, all scripts of this package is covered by `HGS.FloatPoint` namespace.

## Branchs

- `master` -> Keeps the unity project to development purposes.
- `upm` -> Copy of folder content `Assets/Package` to release after pull request in `master`.

Whenever a change is detected on the `master` branch, CI gets the contents of `Assets/Package`, and pushes in `upm` branch.

## Commit Convention

This package uses [semantic-release](https://github.com/semantic-release/semantic-release) to facilitate the release and versioning system. Please use angular commit convention:

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Summary in present tense. Not capitalized. No period at the end.
  │       │
  │       └─⫸ Commit Scope: Namespace, script name, etc..
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

`Type`.:

- build: Changes that affect the build system or external dependencies (example scopes: package system)
- ci: Changes to our CI configuration files and scripts (example scopes: Circle, - BrowserStack, SauceLabs)
- docs: Documentation only changes
- feat: A new feature
- fix: A bug fix
- perf: A code change that improves performance
- refactor: A code change that neither fixes a bug nor adds a feature
- test: Adding missing tests or correcting existing tests
