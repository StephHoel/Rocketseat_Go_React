# Rocketseat API Documentation

This project is a personal challenge to transform the event/course originally in the Go language into C#/.Net.

## Overview

This documentation provides an overview of the Rocketseat API, including its endpoints, request/response schemas, and available operations.

## API Specification

### General Information

- **Title:** Rocketseat.API
- **Version:** 1.0

### Base URL

The base URL for all local endpoints is: `https://localhost:7131`

## Endpoints

### Create Room

- **Endpoint:** `/api/rooms`
- **Method:** `POST`

#### Request Body

```json
{
    "theme": "string"
}
```

- **Content-Type:** `application/json`, `text/json`, `application/*+json`

#### Responses

- **201 Created**

```json
{
    "id": "string"
}
```

- **400 Bad Request**

```json
{
    "errors": [
        "string"
    ]
}
```

### Get All Rooms

- **Endpoint:** `/api/rooms`
- **Method:** `GET`

#### Responses

- **200 OK**

```json
{
    "rooms": [
        {
            "id": "string",
            "theme": "string"
        }
    ]
}
```

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

### Get Room Messages

- **Endpoint:** `/api/rooms/{roomId}/messages`
- **Method:** `GET`

#### Parameters

- `roomId` (path) - UUID of the room

#### Responses

- **200 OK**

```json
{
    "messages": [
        {
            "id": "string",
            "roomId": "string",
            "messageText": "string",
            "reactionCount": 0,
            "answered": true,
            "room": {
                "id": "string",
                "theme": "string"
            }
        }
    ]
}
```

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

### Create Message

- **Endpoint:** `/api/rooms/{roomId}/messages`
- **Method:** `POST`

#### Parameters

- `roomId` (path) - UUID of the room

#### Request Body

```json
{
    "message": "string"
}
```

- **Content-Type:** `application/json`, `text/json`, `application/*+json`

#### Responses

- **201 Created**

```json
{
    "id": "string"
}
```

- **400 Bad Request**

```json
{
    "errors": [
        "string"
    ]
}
```

### Get Message

- **Endpoint:** `/api/rooms/{roomId}/messages/{messageId}`
- **Method:** `GET`

#### Parameters

- `roomId` (path) - UUID of the room
- `messageId` (path) - UUID of the message

#### Responses

- **200 OK**

```json
{
    "message": {
        "id": "string",
        "roomId": "string",
        "messageText": "string",
        "reactionCount": 0,
        "answered": true,
        "room": {
            "id": "string",
            "theme": "string"
        }
    }
}
```

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

### Add React to Message

- **Endpoint:** `/api/rooms/{roomId}/messages/{messageId}/react`
- **Method:** `PATCH`

#### Parameters

- `roomId` (path) - UUID of the room
- `messageId` (path) - UUID of the message

#### Responses

- **204 No Content**

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

### Remove React from Message

- **Endpoint:** `/api/rooms/{roomId}/messages/{messageId}/react`
- **Method:** `DELETE`

#### Parameters

- `roomId` (path) - UUID of the room
- `messageId` (path) - UUID of the message

#### Responses

- **204 No Content**

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

### Mark Message as Answered

- **Endpoint:** `/api/rooms/{roomId}/messages/{messageId}/answer`
- **Method:** `PATCH`

#### Parameters

- `roomId` (path) - UUID of the room
- `messageId` (path) - UUID of the message

#### Responses

- **204 No Content**

- **400 Bad Request**

```json
{
    "errors": [
        "string"
    ]
}
```

- **404 Not Found**

```json
{
    "errors": [
        "string"
    ]
}
```

