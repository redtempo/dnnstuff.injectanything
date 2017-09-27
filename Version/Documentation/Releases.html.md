# Inject Anything Release History 

```Minimum configuration DNN 7.1.2+ (.NET 4.0) / DNN 8+ (.NET 4.5)```

<!-- insert-newversion -->

## 01.05.01

27/Sep/2017


## 01.05.00

22/Jan/2016


* Fixes
	* Added support for DNN8
    * Minimum configuration is now DNN 7.1.2+ (.NET 4.0) / DNN 8+ (.NET 4.5)


For additional release history please visit the [documentation](http://docs.dnnstuff.com/pages/injectanything).


## 01.05.00

22/Jan/2016


* Fixes
	* Added support for DNN8
    * Minimum configuration is now DNN 7.1.2+ (.NET 4.0) / DNN 8+ (.NET 4.5)



```Minimum configuration - DNN 6.0.3+ / DNN 7+ / .NET 3.5```

## 01.04.00

20/Feb/2013

-   Updates
    -   Added DNN7 version compiled against DNN 7.0.0

-   Bug fixes
    -   Fixed issue with escaped square brackets

```Minimum configuration - DNN 5.2.3+ / DNN 6+ / .NET 3.5```

## 01.03.05

13/Jul/2012

-   Enhancements
    -   Updated to support installation on **Azure**
    -   Updated settings layouts to take advantage of DNN6 styling and
        better conform to DNN6 standard

## 01.03.03

04/Apr/2012

-   Enhancements
    -   **Minimum supported install is now DNN 5.2.3 and DNN 6.0.0, both
        have to be .NET Framework 3.5 minimum**

-   Bug fixes
    -   Fixed issue with Upload Script
    -   Fixed bug introduced in 1.3.2 when using standard dnn tokens

## 01.03.02

07/Mar/2012

-   New Features
    -   Added support for Querystring, ServerVariable and Form values
        access
        -   Use [QUERYSTRING:var] or [QS:var] for Querystring access
        -   Use [SERVERVAR:var] or [SV:var] for Servervariables access
        -   Use [FORMVAR:var] or [FV:var] for Form values access

    -   Added debug info for Querystring, ServerVariables and Form
        values
        -   Use [DEBUG:QUERYSTRING], [DEBUG:SERVERVAR] and
            [DEBUG:FORMVAR] respectively to show a list of all available
            tokens and their current values

-   Enhancements
    -   Updated install for DNN5/DNN6
    -   Updated AllTokens standard script to include updated list of
        tokens available

## 01.03.01

09/Aug/2011

-   Fixed DNN6 compatibilities

## 01.03.00

21/Jul/2011

-   Updated to latest version of DNNStuff.Utilities.dll

## 01.02.00

22/Nov/2010

-   Added escaped strings for [ and ] with 0x5B and 0x5D respectively
-   Fixed bug that prevented tokens prompts from refreshing in freeform
    text mode

## 01.01.00

06/May/2010

-   Minimum DNN dependency is now 4.6.2
-   Updated settings to new tab based interface
-   Added Twitter Widget to standard scripts

## 01.00.02

04/Jun/2009

-   Removed 2000 character limit from freeform text insertion

## 01.00.01

10/Feb/2009

-   Added two more tokens
    -   [ROLES] : a comma delimited list of roles the user is in Ex.
        Subscribers,Administrators
    -   [ROLESARRAY] : a delimited list of roles suitable for
        initializing a javascript array
    -   Ex. 'Subscribers','Administrators' so you can use code like var
        myArray = {[ROLESARRAY]}

## 01.00.00

Original Release
