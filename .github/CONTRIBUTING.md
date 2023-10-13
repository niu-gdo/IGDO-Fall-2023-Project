# Contributing to IGDO Fall 2023 Project

This document describes contribution guidelines for the video game project.

## Table of Contents

- [Your First Code Contribution](#your-first-code-contribution)
  - [Unity](#unity)
  - [Set up your code editor](#set-up-your-code-editor)
  - [Add a new scene](#add-a-new-scene)
- [Style Guides](#style-guides)
  - [Branch Naming](#branch-naming)
  - [Commit Messages](#commit-messages)
  - [Pull Requests](#pull-requests)
    - [PR Title](#pr-title)
    - [PR Description](#pr-description)
    - [Embedding Media](#embedding-media)
    - [Adding Reviewers](#adding-reviewers)
    - [Pushing Review Changes](#pushing-review-changes)
    - [Merge Conflicts](#merge-conflicts)

## Your First Code Contribution

Follow these steps to set up your development environment and add a new scene.

### Unity

This project uses the Unity game engine.

[Install Unity Hub](https://docs.unity3d.com/hub/manual/InstallHub.html) and use it to install [LTS Release 2022.3.9](https://unity.com/releases/editor/whats-new/2022.3.9). If you are new to Unity, check out this quick [Introduction to the Editor](https://library.niugame.dev/tutorials/unity-tutorials/unity-editor-introduction/).

### Set up your code editor

Follow the steps to [configure Visual Studio](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity?pivots=windows) or your editor of choice.

### Add a new scene

When working on a new feature that requires testing in a Unity scene, please create a duplicate of the `DeveloperTemplate` file (Located at `Scenes/DeveloperTemplate`). You can rename the scene and test your changes there.

This allows you to test your changes in an isolated space, as two developers making changes to the same scene can cause a terrible headache of inconsequential merge conflicts. It also gives us a very clear location where we can test your changes!

## Style Guides

Please follow these guidelines when contributing changes the the project.

### Branch Naming

Follow best practices for naming your git branches. Ideally, they should be in this format:

`[optional-grouping-prefix/]<issue-number>-<issue-description>`, eg:
`26-create-coding-conventions-page`

Your branches should be named according to the following guidelines:

* **May** include an optional prefix of `add/`, `docs/`, `fix/`, or other common grouping prefix. (Not required)
* **Should** include the issue number (if one exists) before any other non-prefix words.
* **Must** refer to the included changes.
* **Must** limit the branch name to 50 characters.
* **Must** use [kebab case](https://developer.mozilla.org/en-US/docs/Glossary/Kebab_case) to separate words.
* **Must not** include your name or generic names such as "test-branch".

#### Branch Naming Examples

| Issue # | Issue type    | Issue description             | Branch name example      | Example with grouping prefix |
|--------:|---------------|-------------------------------|--------------------------|------------------------------|
|       7 | feature       | Create airlock system         | 12-add-airlock-system    | add/12-airlock-system        |
|      13 | bug           | Player can move out of bounds | 13-fix-player-oob-glitch | fix/13-player-oob-glitch     |
|      14 | documentation | Update README                 | 14-update-readme         | docs/14-update-readme        |

| :information_source: NOTE |
|:---|
| If you [use the GitHub web UI to create a branch from the issue page](https://docs.github.com/en/issues/tracking-your-work-with-issues/creating-a-branch-for-an-issue#creating-a-branch-for-an-issue), <br> it will automatically create a branch named in this format. |

### Commit Messages

Follow [The seven rules of a great Git commit message](https://cbea.ms/git-commit/#seven-rules):

> 1. [Separate subject from body with a blank line](https://cbea.ms/git-commit/#separate)
> 2. [Limit the subject line to 50 characters](https://cbea.ms/git-commit/#limit-50)
> 3. [Capitalize the subject line](https://cbea.ms/git-commit/#capitalize)
> 4. [Do not end the subject line with a period](https://cbea.ms/git-commit/#end)
> 5. [Use the imperative mood in the subject line](https://cbea.ms/git-commit/#imperative)
> 6. [Wrap the body at 72 characters](https://cbea.ms/git-commit/#wrap-72)
> 7. [Use the body to explain what and why vs. how](https://cbea.ms/git-commit/#why-not-how)

#### Commit Message Example

```sh-session
git commit -m "Implement gravity feature

* Add gravity generator
* Add gravity demo scene

I did it this way because blah blah blah."
```

### Pull Requests

Pull requests allow you to bring your branch's changes into our main branch.

#### PR Title

##### Guidelines

* Keep the title concise and to the point.
* Be descriptive of the change.
* If you can't accurately describe the change in 50 characters, it's probably too big for a single pull request.
* Referring to the issue in the subject is optional. You can do it in the body instead.
* Capitalize the first letter.

##### Examples

* Add airlock system mechanic
* Fix bug where player could move OOB
* Update screenshot links in docs

#### PR Description

Your description should help reviewers and those watching the repo figure out the following from your pull request:

* The purpose of the pull request.
* What the included changes will do or enable.
* (If in Unity Editor) What scenes your changes can be found in.
* Specifically what you want feedback on.
* Who you want included in the discussion for the request (@mention, request reviewers)

Remember that people in the organization will use your pull request to track changes to our codebase, so please be descriptive and somewhat professional with your descriptions.

| :exclamation: IMPORTANT                                          |
|:-----------------------------------------------------------------|
| Include a reference to the issue that the PR is meant to resolve |

##### PR Description Example

> Hey team, this branch implements feature [#1337](#) to enhance gameplay with the Jetpack Module. Leveraging Unity's advanced physics engine, we've created a control system for dynamic navigation using thrust vectors, velocity adjustments, and visual effects.
> 
> Here's a GIF of what it looks like in a scene:
>
> ![animated GIF of jetpack](https://github.com/niu-gdo/IGDO-Fall-2023-Project/assets/59938457/a2d46b37-c6b2-41b9-a965-a42a2018b7ee)>
>
> We had to reticulate some splines in order to sync the thrusters with the retroencabulators, but we're planning on addressing that in the next PR.
> 
> Can we get some :eyes: on this? Tagging [@overlords](#) for visibility!
> 
> Part of [#1337](#)

#### Embedding Media

**Where ever possible, include media to demonstrate your changes**. Use screenshots and gifs to allow people to preview your changes without pulling the branch.

* [How to take a screenshot on any platform](https://www.take-a-screenshot.org/)
* [ScreenToGif](https://www.screentogif.com/) - GIF recording software
* [ShareX](https://getsharex.com/) - Screen recording software

You can embed a gif directly into the body of your pull request and it will be hosted on GitHub (forever).

#### Adding Reviewers
Adding reviewers to a pull request can help signal who you want to examine, test, or be aware of your changes. In general, mark the following people are reviewers:

* At minimum, **at least ONE person with write access to the repository**.  These are typically IGDO organization officers. You **need** approval from one of them to clear your request. Feel free to request review from *all* of the officers.
* If you worked in a group, **assign to** all members of the group.
* If you are making changes to an existing file, consider tagging that file's owner.
* Anyone else who may have useful insight or context on the issue.

#### Receiving Feedback
When a review starts on your pull request, the reviewer will do two things:
1. Run your branch in the Unity Editor and test how the game runs, as well as whatever new features / bugs were addressed.
2. Analyze every file changed in order to spot potential bugs, bad design or inconsistent format.

When running the game, the reviewer will use whatever scene you mentioned in the description to find your changes. As was mentioned, please make it obvious where and how your changes can be tested to make the process smooth and fast.

When reviewing files, reviewers can start *conversations* on entire files, or on code snippets within the files. These conversations will pose suggestions, questions, or <ins>required changes</ins>. You can also leave responses to these conversations and resolve them (which you probably shouldn't do, let the original reviewer do this).

Ultimately, after the review is done, you will have one of two outcomes:

* **Changes Requested**: One or more conversations added to a review need to be addressed, and the changes cannot yet be merged.
* **Approval**: Your changes are fully reviewed and ready to merge. Assuming no merge conflicts, you can press the green button to merge.


#### Pushing Review Changes

Your branch is not frozen when you create a pull request. Pushing commits to the same branch will update the pull request and mark any previous reviews as "stale" (This will also shake off an approval status, as well!).

Note that this is how you will respond to requested changes on a review; just push the new changes to the same branch. Note that you will have to request review from the same people again.

#### Merge Conflicts

Once your PR is approved, you may find that your pull request has *merge conflicts* with the main branch. These are code changes which cannot be trivially added into the main branch and must be manually resolved.

The recommended way to solve this is locally. Do the following with your Git tool:
1. Checkout `main` and do `$ git pull` to get the most up-to-date main commit.
2. Checkout to the pull-request branch.
3. `$ git merge main`.
4. This *should* begin a merge conflict. If not, there never was one, congratulations!
5. Resolve each listed merge conflict.
6. Commit and **TEST THE NEW CHANGES**.
7. Push and request review again.

You can also use the GitHub Web Tool to resolve minor merge conflicts, but always remember to go back and *test after resolving conflicts*.
