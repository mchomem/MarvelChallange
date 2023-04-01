# MarvelChallange

MarvelChallenge is a challenge to query the Marvel api and extract some information about characters and comic book titles.
Credentials (public and private key) are required to consume Marvel API, more information can be started at [Marvel Developer](https://developer.marvel.com/).
You will have to create an account and get the information from [Marvel credentials](https://developer.marvel.com/account)

## Getting Started: How to use

Build the application using `Visual Studio 2022`. After successful construction, the binaries can be published to an application server such as IIS.
Ask the systems administrator to provide the url to access the MarvelChallenge api rest.
After getting the api keys from your Marvel Developer account, configure the project file `appsettings.json` with the information `apikey` (public key), `ts` which can be any timestamp and can be easily obtained from the website [Roger Takemiya](https://rogertakemiya.com.br/converter-data-para-timestamp/) and finally the `hash` which is the combination of timestamp + private key + public key (in md5 format).
To generate the md5 just access the [MD5 hash](https://www.md5.cz/) website.

### Routes and features

|  Nº |                             URL                     |					Function                                                                                |
|-----|-----------------------------------------------------|-----------------------------------------------------------------------------------------------------------|
|  1  | https://(url_server):(port)/MarvelChallange/import  | Imports the data needed to text file                                                                      |
|  2  | https://(url_server):(port)/MarvelChallange         | Get all data from the Marvel api accessing the end-point https://gateway.marvel.com/v1/public/characters  |

## Development Team

-   [Misael C. Homem](https://www.linkedin.com/in/misael-da-costa-homem-8b07a158/) (Developer, Analyst, Architect)

## Features

-   Query data directly from the Marvel API;
-   Imports marvel character data to text file;


## Technologies Used

-   C#. net core
-   ASP.NET core api rest
-   Swagger

## How to Contribute

1.  Fork the project
2.  Clone the fork to your local machine
3.  Create a branch for your changes: `git checkout -b my-branch`
4.  Make changes and commit: `git commit -am "My changes"`
5.  Push your branch to your fork: `git push origin my-branch`
6.  Open a Pull Request in the original repository
7.  Wait for your Pull Request to be reviewed and merged
