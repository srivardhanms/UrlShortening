# Pairing Exercise

## Software Engineers &amp; Testers

We’d like to test your mettle by asking you to complete a **technical exercise**. You should aim to spend around **55 minutes** on this technical test.

You will be pairing with a member of the APL team. Ask them any questions you need to be able to deliver the solution. **You will not be marked down for asking questions!**

### Brief

In this exercise, you will be working on an implementation of a URL shortening service. You can use **any language** or **any framework** or **any tools** of your choice.

You’re going to deliver a URL shortening service where you enter a URL and it returns a shortened URL. For example: [https://allaccess.keepup.com.au/detail/videos/all-access/video/6316092658112/a-leagues-all-access-%7C-episode-07:-be-strong-girl](https://allaccess.keepup.com.au/detail/videos/all-access/video/6316092658112/a-leagues-all-access-%7C-episode-07:-be-strong-girl ) might get shortened to [https://keep.up/ff342z9](https://keep.up/ff342z9).

There are no restrictions on how the encode or decode algorithms should work. We just need to ensure that a URL can be encoded to a short URL and the short URL can be decoded to the original URL.

**The shortened URLs do not need to be persisted to a database. They will be kept in memory for the purposes of this exercise.**

### Non-functional requirements

The features, below, were written by a Product Owner. As a member of a software engineering team, you are responsible for the non-functional requirements of the product. Please take them into consideration when working on your solution.

### Note for Software Engineers

APL teams practise test-first. We are aware that some candidates don’t practise this in their current roles. You therefore do not need to write your solution test-first, but you should write tests for your solution to be confident that it works.
  
## Features

### Encode

This endpoint should encode a URL to a shortened URL. • Accepts a JSON body.

- Accepts a request with a URL in the body.
- Returns a JSON response.
- Returns a response with a shortened URL in the body.
- Returns the same shortened URL for a URL that has already been shortened.

> Don’t forget non-functional requirements.

### Decode

This endpoint should decode a previously shortened URL.

- Accepts a JSON body.
- Accepts a request with a shortened URL in the body.
- Returns a JSON response.
- Returns a response with the original URL corresponding to the shortened URL in the body.

> Don’t forget non-functional requirements.

### Follow

The last endpoint should allow me to follow a shortened URL to the original URL.

- Accepts requests to previously shortened URLs.
- Redirects responses to the original URL.

> Again: please pay close consideration to non-functional requirements for an endpoint of this type.
