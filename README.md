# ASP.Net REST API Overflower

## Description

API returning tag data from StackOverflow.

> Overflower is using ASP.Net 8

### Features:
* downloads the 1000 most popular tags from the StackOverflow API
* calculation of the % share of the tag in the pool taken
* sorting by tag name
* paging

## How to setup

1. Install `Docker`
   * for `Windows` - Install `DockerDesktop`

## How to run

1. Open terminal
1. Go to your `.sln` directory (directory with `docker-compose.yaml` file)
1. Run `docker-compose`
   ```bash
   docker compose up -d
   ```
1. Run your application using your favorite IDE or terminal `cd YourName.Api && dotnet run`
1. Go to [Swagger](http://localhost:5024/swagger/index.html)
1. Check if it looks good

### Development
I am happy to accept suggestions for further development. Please feel free to add Issues :)

### Authors
- [Katarzyna Kądziołka](https://github.com/Katarzyna-Kadziolka)

### License
This project is licensed under the MIT License - see the [LICENSE](https://raw.githubusercontent.com/Katarzyna-Kadziolka/Overflower/main/LICENSE) file for details.
