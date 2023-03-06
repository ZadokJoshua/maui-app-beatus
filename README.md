### Beatus
A .NET MAUI Plant leaf disease detection application. This application leverages third-party tools like Custom Vision AI and Open AI-GPT3.


<p align="center">
<kbd>
<img src="/Images/20230306_161402.jpg" Height=300/>
</kbd>
<kbd>
<img src="/Images/20230306_162630.jpg" Height=300/>
</kbd>
<kbd>
<img src="/Images/20230306_161223.jpg" Height=300/>
</kbd>
</p>

### Getting Started
To get started with Beatus, follow these steps:

* Clone this repository and open it in Visual Studio 2022.

* Ensure that you have .NET 7 installed on your machine.

* Create an appsettings.json file within the project. The file should be in this format:
    ```js
    {
  "CustomVision": {
    "Endpoint": "<Custom Vision Endpoint>",
    "Key": "<Custom Vision API Key>"
  },
  "OpenAI": {
    "Endpoint": "<OpenAI API Endpoint>",
    "Key": "<OpenAI API Key>"
  }
  }
    ```
    
    The Endpoint should contain the URL for the API endpoint, while the Key should contain the API key for authentication.
    
* Build and run the application. 
### Datasets:
* Tomato leaf diseases - https://data.mendeley.com/datasets/369cky7n39/1
* Cassava leaf diseases - https://data.mendeley.com/datasets/3832tx2cb2
* Corn leaf diseases - https://www.kaggle.com/datasets/vipoooool/new-plant-diseases-dataset?resource=download
