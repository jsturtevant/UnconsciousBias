# UnconsciousBias

You know how when you become comfortable with someone, you sometimes may become a little more blunt?  

Sometimes we may not notice if the tone of our emails is overly positive or negative. It's important to occasionally stop and reflect that we are treating the people around us with respect. The Unconscious Bias project is a Windows universal application allows you to use data to analyze the sentiment of your emails to a given email address. 

[James Sturtevant](http://www.jamessturtevant.com/) and [Jennifer Marsman](https://blogs.msdn.microsoft.com/jennifer/) developed “Unconscious Bias” at the hack.nyc event on April 13-14, 2016.  We created a Windows universal application in which the user can specify an email address.  Then, we use the [Office 365 APIs](http://dev.office.com/getting-started/office365apis) to access the user’s email and get messages that were sent to that email address.  The emails are analyzed for positive/negative sentiment by the [Microsoft Cognitive Services Text Analytics](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) service.  Then we display the average sentiment score of your emails to that email address, along with a chart of your sentiment over time.  

## Setup
You will need [Visual Studio 2015 Community](UnconsciousBias/UnconsciousBias/Helpers/TextAnalyticsHelper.cs) with Windows 10 SDK installed (install through advanced options learn more at [Visual Studio Shorts](https://channel9.msdn.com/Blogs/Visual-Studio-Shorts/Installing-Visual-Studio-2015-Community) on [Channel9](https://channel9.msdn.com)).

1. Obtain your Office 365 API from the [App Registration portal](http://dev.office.com/app-registration)
2. Add your ```ClientId``` and ```Return URL``` to [UnconsciousBias/UnconsciousBias/Styles/Secrets.xaml](https://github.com/jsturtevant/UnconsciousBias/blob/master/UnconsciousBias/Styles/Secrets.xaml)
3. Obtain a Cognitive Services ```Text Analytics``` API Key from [Cognitive Services Subsciptions](https://www.microsoft.com/cognitive-services/en-US/subscriptions)
4. Add the key to ```AccountKey``` Property in [UnconsciousBias/UnconsciousBias/Helpers/TextAnalyticsHelper.cs](https://github.com/jsturtevant/UnconsciousBias/blob/master/UnconsciousBias/Helpers/TextAnalyticsHelper.cs)
5. Sign up for free Telerik Trail and download [UWP UI Toolkit](http://www.telerik.com/universal-windows-platform-ui) (see issue #3)
6. Build and Run

## Privacy Policy
You can find our latest Privacy Policy at http://aka.ms/bias.

## Image
Image: [Injustice by Luis Prado from the Noun Project](https://thenounproject.com/search/?q=bias&i=89997)

## Template 10 
[Template 10](https://github.com/Windows-XAML/Template10) was used in the project.

## Cognitive Services Terms and Developer Code of Conduct
[Cognitive Services TOU](http://research.microsoft.com/en-us/um/legal/CognitiveServicesTerms20160628.htm) and [Developer Code of Conduct](http://research.microsoft.com/en-us/UM/legal/DeveloperCodeofConductforCognitiveServices.htm)
