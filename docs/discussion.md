# Discussion

_In a typical scenario, this discussion would involve the stakeholders and the development team to gather
their input and align on the project's goals and requirements. However, for the purpose of this exercise,
this discussion was done alone (with some imagination)._

The exercise being called "Paynext 2" suggests that there might be plans to replace the
existing payment service, Paynext, with a new and improved version. This opens up the possibility of implementing
additional features and enhancements beyond just credit card validation at a later stage.

However, considering the stakeholder being a well-established company with a large client base and with an interest in building
a second version of "Paynext" it points towards the aim for a more scalable and modular approach.
Therefore, I came to the decision to make a minimal API to satisfy that need.

In conclusion, a minimal API approach focusing only on payment validation is recommended to deliver a reliable MVP
in a shorter timeframe. This approach provides a solid foundation for future enhancements and ensures
flexibility and adaptability to evolving payment trends and requirements.

## Design Decisions

- **Minimal API**: Chose a minimal API approach to keep the implementation lightweight and straightforward.
- **Service Layer**: Separated the validation logic into a service class to promote modularity and ease of testing.
- **Validation Logic**:
  - Implemented basic null checks for required fields.
  - **Card Number** (_Details on [docs/research.md](research.md) page_)
    - Checked if it follows the Luhn algorithm.
    - Identified the card type based on common card number patterns.
  - **CVC**: Matches the expected length for each card type.
  - **Expire Date** _(Previously Issue Date [Read More](#other))_:
    - Follows the format MM/yy.
    - Checked if the card is expired based on the current date.

## Other

- **Issue Date**: The specification refers to an Issue Date which would mean the date when the card was issued, information the cardholder usually does not remeber or have. After some concideration the field will be changed to "ExpireDate".
