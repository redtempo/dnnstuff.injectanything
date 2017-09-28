
# Inject Anything Standard Script 



### What are Standard Scripts?

These are scripts that are included Out of the Box (OOTB) with the
install of Inject Anything. Each script included is ready to go and is
meant to target a specific need within your site. Some scripts are for
tracking web analytics, some are for social bookmarking and some are
just for demonstration purposes.

These scripts differ slightly from normal javascript (.js), vbscript
(.vbs) and css files (.css) because they are packaged up in an xml file
which helps provide a description of the script, help in how to use it
and other files that might be used with the script including images etc.

### Standard Script Format

	<?xml version="1.0" encoding="utf-8" ?>

	<script>
	<description>Description of script</description>
	<help>Provide some help as to how to use the script</help>
	<items>
		<item>
			<content>
			<![CDATA[
				Here is the content of my script.
				Putting the content within a CDATA is necessary so that <script>` sections etc.
	            are not parsed as part of the xml document.
	       ]]>
			</content>
		</item>
	</items>
	</script>


If the description, help or content requires any html you may have to
surround it in a CDATA section as shown in the above image.

A standard script may also require additional image files etc. which can
be included in a sub directory of the root Standard folder. In your
script if you wish to refer to the Standard folder, use the
[STANDARDFOLDER] token in your script. The [STANDARDFOLDER] token will
resolve to the standard folder at runtime.

If you wish to upload the script to your site, create a zip file in the
proper format and upload the script to your site using the
[Upload Script](UploadScript) function.
