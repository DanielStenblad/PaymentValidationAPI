# PaymentValidationAPI

## Table of Content

- [Summary](#summary)
- [Features](#features)
- [Technical Requirements](#technical-requirements)
- [Discussion](#discussion)
- [Research](#research)
- [Roadmap](#roadmap)

## Summary

_This exercise is based on the specifications provided in the NET_Paynext 2.pdf document found in the docs folder._

The aim of this exercise is to create an API for a payment application that validates credit card information.
The application as a whole is designed to simplify client payments by allowing merchants to generate payment links,
which clients can use to verify order data and complete payments.
This API specifically focuses currently on validating credit card data, ensuring that all required fields are provided,
the card is not expired, and the card number and CVC are valid.
The API returns the card type upon successful validation or a list of validation errors if the input data is invalid.

## Features

- Validate credit card information including card owner, card number, expire date and CVC.
- Ensure the card is not expired.
- Indentify card type by card number (Supported types: MasterCard, Visa and American Express).
- Verify the CVC length based on the card type.
- Return the card type upon successful validation.
- Return detailed validation errors if the input data is invalid.

## Technical Requirements

- Implement the API using C# and .NET Core.
- Provide automated tests as a bonus.

## Discussion

For information on how I decided on my approach read more in the [docs/discussion.md](docs/discussion.md) document.

## Research

For more information on the research and sources for this solution read more in the [docs/research.md](docs/research.md) document.

## Roadmap

For suggestions on further development on this application, please refer to the roadmap in the [docs/roadmap.md](docs/roadmap.md) document. These points have been considered to be outside the scope of this exercise but are worth mentioning.
