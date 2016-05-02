# UnconsciousBias

You know how when you become comfortable with someone, you sometimes may become a little more blunt?  

Sometimes we may not notice if the tone of our emails is overly positive or negative. It's important to occasionally stop and reflect that we are treating the people around us with respect. The Unconscious Bias project is a Windows universal application allows you to use data to analyze the sentiment of your emails to a given email address. 

James Sturtevant and Jennifer Marsman developed “Unconscious Bias” at the hack.nyc event on April 13-14, 2016.  We created a Windows universal application in which the user can specify an email address.  Then, we use the Office 365 APIs to access the user’s email and get messages that were sent to that email address.  The emails are analyzed for positive/negative sentiment by the Microsoft Cognitive Services Text Analytics service.  Then we display the average sentiment score of your emails to that email address, along with a chart of your sentiment over time.  
