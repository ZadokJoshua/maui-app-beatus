# Beatus
This plant leaf disease detection application was built using .NET MAUI, C#, and third-party tools such as Custom Vision and OpenAI GPT-3. The custom vision model used in the application was trained using labeled images of tomato, cassava, and corn leaf diseases.

When the model makes a correct prediction, the application passes the Tagname obtained to the OpenAI GPT-3 API. OpenAI GPT-3 generates recommended tips for the current condition of the plant, providing valuable insights into how best to manage the disease and ensure the plant's health. Predictions can also be saved to a database for off-line access.

<p align="center">
<kbd style="margin: 4px; background: #282828;">
<img src="/Images/20230306_161402.jpg" Height=400/>
</kbd>
<kbd style="margin: 4px; background: #282828;">
<img src="/Images/20230306_162630.jpg" Height=400 />
</kbd>
<kbd style="margin: 4px; background: #282828;">
<img src="/Images/20230306_161223.jpg" Height=400/>
</kbd>
</p>

## Getting Started
To get started, follow these steps:

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
## Datasets:
The custom vision model was trained using the following datasets:
* Tomato leaf diseases - https://data.mendeley.com/datasets/369cky7n39/1
* Cassava leaf diseases - https://data.mendeley.com/datasets/3832tx2cb2
* New Plant Diseases Dataset (Corn) - https://www.kaggle.com/datasets/vipoooool/new-plant-diseases-dataset?resource=download

#
*Please note that the app has not been tested on Mac or iOS devices, although it was developed using .NET MAUI, which is a cross-platform framework, it's still possible that some features may not work as expected on these platforms.*
