### Beatus
Plant leaf disease detection application built using .NET MAUI, C# and third party tools like Custom Vision AI and Open AI-GPT3.

<kbd align="center">
<p align="center">
<img src="/Images/20230306_161402.jpg" Height=250/>
<img src="/Images/20230306_161434.jpg" Height=250/>
<img src="/Images/20230306_161223.jpg" Height=250/>
</p>
</kbd>

### Getting Started
* After opening this repository in Visual Studio 2022, create an appsettings.json file within the project. The file should be in this format:
    ```js
    {
      "CustomVision": {
        "EndPoint": "",
        "Key": ""
      },
      "OpenAI": {
        "Endpoint": "",
        "Key": ""
      }
    }
    ```
### Datasets:
* Tomato leaf diseases - https://data.mendeley.com/datasets/369cky7n39/1
* Cassava leaf diseases - https://data.mendeley.com/datasets/3832tx2cb2
* Corn leaf diseases - https://www.kaggle.com/datasets/vipoooool/new-plant-diseases-dataset?resource=download
