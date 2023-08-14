# Backend Development Assignment

## How to run?

In project directory
```bash
 $ dotnet run
```

Monitor Swagger in your web browser.

```bash
 http://localhost:{port}/swagger/index.html
```


## Endpoints

▪ Search (open, not secured) HTTP-GET

Search for movies by title on The Open Movie Database.

Return the movie information in the specified JSON format.
```bash
curl -X 'GET' \
  'http://localhost:5047/search?movieTitle=Barbie' \
  -H 'accept: */*'
```
Response Body :

```bash
{
  "title": "Barbie",
  "year": "2011",
  "rated": "R",
  "released": "25 Oct 2012",
  "genre": "Drama",
  "director": "Sang-woo Lee",
  "writer": "Sang-woo Lee",
  "actors": "Cheon-hee Lee, Kim Sae-ron, Ah-ron Kim",
  "plot": "Soon-Young (Kim Sae-Ron) is a young girl who is the head of her family. She lives with her mentally handicapped father (Jo Yong-Suk), unscrupulous uncle (Lee Chun-Hee) and younger sister Soon-Ja (Kim Ah-Ron) who is always ill. Younger sister Soon-Ja plays with her Barbie doll everyday and dreams of one day living in the United States. Meanwhile, Mang-Taek comes into contact with an American man interested in adopting a healthy Korean girl. Mang-Taek arranges a deal for the American man to adopt Soon-Young. When her younger sister Soon-Ja hears of the adoption, she becomes jealous and asks to take the place of her older sister. When the American man and his young daughter arrive to take Soon-Young, the uncle, Soong-Young and Soon-Ja must decide who is to go. The American father has an ulterior motive for the adoption....",
  "language": "Korean",
  "country": "South Korea",
  "awards": "1 win & 1 nomination",
  "poster": "https://m.media-amazon.com/images/M/MV5BYmRhMTRmM2QtMzRlNC00MzJjLTljMzYtNmY0MzMzZmZlMmE0XkEyXkFqcGdeQXVyMjAzMjcxNTE@._V1_SX300.jpg",
  "imdbRating": "6.7",
  "imdbVotes": "287",
  "imdbID": "tt2074334"
}
```

▪ HTTP-GET to get all stored request entries

Get all movies as an admin and append the apiKey(1088) to the get request.

Return the movies.
``` bash
curl -X 'GET' \
  'https://localhost:7244/Movie/1088' \
  -H 'accept: */*'
```
Response Body :
``` bash
[
  {
    "id": "64d7cce1b62a3d5ec8a276f9",
    "search_token": "Hobbit",
    "imdbID": "1111",
    "processing_time_ms": 590,
    "timestamp": "2011-06-09T21:00:00Z",
    "ip_address": "localhost"
  },
  {
    "id": "77d7cce1b62a3d5ec8a276f9",
    "search_token": "Titanic",
    "imdbID": "1233",
    "processing_time_ms": 456,
    "timestamp": "2023-08-12T22:38:12.572Z",
    "ip_address": "localhost"
  }
]
```

▪ HTTP-GET to get a single request entry

Get a movie as an admin by searching for its title. The apiKey(1088) must be used to authenticate. Return the movie.

``` bash
curl -X 'GET' \
  'https://localhost:7244/Movie/1088/search/title/Titanic' \
  -H 'accept: */*'
```
Response Body :
``` bash
{
  "id": "77d7cce1b62a3d5ec8a276f9",
  "search_token": "Titanic",
  "imdbID": "1233",
  "processing_time_ms": 456,
  "timestamp": "2023-08-12T22:38:12.572Z",
  "ip_address": "localhost"
}
```
▪ HTTP-GET to search on date period

Get movies as an admin, returning only those movies whose timestamps are within the period definition.

The apiKey(1088) must be used to authenticate.

``` bash
curl -X 'GET' \
  'https://localhost:7244/Movie/1088/search/period?startYear=2000&startMonth=01&startDay=03&endYear=2023&endMonth=07&endDay=01' \
  -H 'accept: */*'
```

Response Body : 
``` bash
[
  {
    "id": "64d7cce1b62a3d5ec8a276f9",
    "search_token": "Hobbit",
    "imdbID": "1111",
    "processing_time_ms": 590,
    "timestamp": "2011-06-09T21:00:00Z",
    "ip_address": "localhost"
  }
]
```
▪ HTTP-GET to report usage on per day (DD-MM-YYYY)

The apiKey(1088) must be used to authenticate as an Admin.

``` bash
curl -X 'GET' \
  'https://localhost:7244/Movie/1088/counter' \
  -H 'accept: */*'
```
Response Body:

``` bash
[
  {
    "date": "2023-08-13",
    "count": 1
  },
  {
    "date": "2011-06-09",
    "count": 1
  },
  {
    "date": "2023-08-12",
    "count": 2
  }
]
```
▪ HTTP-DELETE to delete an request entry

The apiKey(1088) must be used to authenticate as an Admin.

``` bash
curl -X 'DELETE' \
  'https://localhost:7244/Movie/1088/1111cce1b62a3d5ec8a276f9' \
  -H 'accept: */*'
```

▪ BONUS ENDPOINT 

HTTP-GET to get movie by imdbId.
The apiKey(1088) must be used to authenticate as an Admin.

``` bash
curl -X 'GET' \
  'https://localhost:7244/Movie/1088/search/ImbdId/1233' \
  -H 'accept: */*'
``` 
Response Body:
``` bash
{
  "id": "77d7cce1b62a3d5ec8a276f9",
  "search_token": "Titanic",
  "imdbID": "1233",
  "processing_time_ms": 456,
  "timestamp": "2023-08-12T22:38:12.572Z",
  "ip_address": "localhost"
}
``` 

## What happens when the apiKey is not correct?
``` bash
response status : 401

response body:
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401,
  "traceId": "00-6b35fff85b3f858c02b0708bfb60140d-9d6589c16618f21d-00"
}
``` 

## Time Management

I planned to finish the assignment in 2 days.

I spent two days on this assignment(08.12 - 08.13).

08.12.2023

The first day was mostly dedicated to researching (best)practices and starting to create the project architecture. 


08.13.2023

On the second day, I completed the task.
