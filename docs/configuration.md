# Inject Anything Configuration 

## Insertion Point

The insertion point is the position at which the content is inserted
into the page. The options are:

-   **Module** (default)
	- the content will be inserted where ever the module resides in the page
-   **Head**
	- the content will be inserted within the ```<head></head>``` tags
-   **After Opening Form**
	- the content will be inserted right after the ```<form>``` tag
-   **Before Closing Form**
	- the content will be inserted right before
    the    ```</form>```
    tag
-   **After Opening Body**
	- the content will be inserted right after
    the ```<body>```    tag
-   **Before Closing Body**
	- the content will be inserted right before
    the ```</body>``` tag

## Source

The source field refers to the source of the content. The options are:

-   **Standard Script**
	- standard scripts are scripts or content that
    are packaged and included with the standard install of Inject
    Anything. These provide quick prebuilt and tested scripting
    functionality out of the box.
-   **File Explorer**
	- this option allows you to upload and include
    scripts of your own making. For instance, lets say that you find a
    neat script on a javascript site and want to make that available to
    your pages. You would use File Explorer to upload the script to the
    DNN filesystem and then select it using this option.
    -   Note: By default javascript files (.js) are not allowed as a
        valid upload. To enable upload of .js files you need to log in
        as host, go to Host Settings|Advanced Settings|Other Settings,
        File Upload Settings and add .js to the list of valid file
        extensions. If you feel this is a security risk in your portal
        situation, then use an ftp application to upload them directly
        to your /portals/0 folder.
-   **Freeform Text**
	 - selecting this option will enable a text box
    where you can enter your script, content etc. directly into the
    module

## Enable

Select this option to enable the injection of the content. It's
essentially a quick way to stop the insertion of your content
temporarily without having to delete the module.

## Content Tokens

The area will show any content token inputs that are available for the
current content. Content tokens are tokens placed within the content
that are replaced at runtime with the values entered against them. They
are a powerful way to add flexibility to any script.

For instance, instead of creating a simple Google Analytics script that
has your Google Analytics Web Property ID (the UA-?????-? number)
hardcoded directly into the script, you can instead insert a token such
as [WEBPROPERTYID] in the content and the Inject Anything module will
show an input for WEBPROPERTYID in this content token section for the
user to fill in. When the script is injected at runtime, the token is
replaced with it's value. This allows you to create a single script once
with a variable (token) and use it for any portals you wish. All you
have to do is vary the value for each module you use the script with.

A more complete examination of tokens is included in the token topic.

## Debug

Selecting this option will display your content inside of `<pre>` tags so you can see exactly what is being inserted into your
page.

![Inject Anything Configuration](images\Configuration.png)
