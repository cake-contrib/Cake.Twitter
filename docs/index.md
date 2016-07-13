# Cake.Twitter Documentation

Cake.Twitter is an Addin for [Cake](http://cakebuild.net/) which allows the posting of a Tweet to Twitter.

To use this addin, you will need to add the following directive in your build.cake script:

```csharp
#addin Cake.Twitter
```

This will instruct Cake to download the [Cake.Twitter](https://www.nuget.org/packages/Cake.Twitter/) package from NuGet and install it into the Cake Addin's folder, and from there, load the associated assembly in the execution context for consumption within your script.

<div class="admonition note">
    <p class="first admonition-title">Note</p>
    <p class="last">
        This Addin would not have been possible without the help of this [blog post](http://www.thatsoftwaredude.com/content/6289/how-to-post-a-tweet-using-c-for-single-user).  A big thank you to Alex who showed how to interact directly with the Twitter REST API.
    </p>
</div>

<div class="admonition attention">
    <p class="first admonition-title">Attention</p>
    <p class="last">
        In order to make use of this Addin, a number of security tokens, and keys, are required.  Each one of these is detailed in the blog post mentioned above, and I would encourage you to read through that blog post, prior to trying to use this Addin.
    </p>
</div>