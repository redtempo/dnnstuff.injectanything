
# Inject Anything Tokens 

What are tokens?
----------------

See [Tokens](/pages/tokens) for a general overview of tokens and the
standard token support across most DNNStuff modules

Inject Anything Specific Tokens
-------------------------------

-   [StandardFolder]
    -   inserts the relative path to the /DNNStuff -
        InjectAnything/standard folder. This token is used in many
        standard scripts for inclusion of images etc.

Content Tokens
--------------

Content tokens are tokens that are specific to the content that is being
injected. When content is injected, the content is inspected for user
tokens and are replaced with the corresponding values.

The values that are used to replace the tokens are entered by the module
editor within the module settings.

A content token is any word that is included in square brackets []. You
can include as many of these content tokens within your content and you
will be prompted for their replacement values in the Inject Anything
options screen.

Tokens can optionally include a couple of different of attributes which
make the value prompting much easier and user friendly.

Optional attributes:

-   Caption - normally the prompt caption for the token is the token
    name itself, but if you include a caption attribute the options
    screen will use it instead for the label of the input.
    -   Example - [MYTOKEN caption="Token Value"]

-   Help - if you include this attribute the help icon to the left of
    the label of the input will be populated with your help message.
    -   Example - [MYTOKEN caption="Token Value" help="Enter the value
        of the token"]


