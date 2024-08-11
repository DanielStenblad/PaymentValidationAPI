# Research

- This file is used for gathering and documenting information gathered while working on this project.
- It serves as a record of the knowledge and insights gained during the problem-solving process.
- The information collected here can be used for future reference.

# Credit Cards

## Card Number Validation

### Luhn Algorithm 

The Luhn algorithm, also known as the "modulus 10" or "mod 10" algorithm, is a simple checksum formula used to validate a variety of identification numbers, such as credit card numbers. 
The algorithm is designed to protect against accidental errors and fraud. It is not intended to protect against malicious attacks.

#### Sources

- https://en.wikipedia.org/wiki/Luhn_algorithm


## Identifying VISA

```regex
^4[0-9]{12}(?:[0-9]{3})?$
```

### Explanation of the Regex Pattern:

- **^**: Asserts the start of the string.
- **4**: The first digit of a Visa card number is always 4.
- **[0-9]{12}**: Matches exactly 12 digits following the first digit (total 13 digits so far).
- **(?:[0-9]{3})?**: Optionally matches an additional 3 digits, making the total 16 digits if they are present.
- **$**: Asserts the end of the string.

### Details:

- **13-digit card**: Visa used to issue 13-digit card numbers, though this is rare today. The regex accounts for this by allowing for only 12 digits following the initial 4.
- **16-digit card**: The most common length for Visa cards is 16 digits. The regex handles this by optionally allowing for three additional digits after the first 13 digits.
- **CVC**: Uses 3 digits

### Sources:

- https://www.regular-expressions.info/creditcard.html

## Identifying Master Card

```regex
^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$
```

### Explanation of the Regex Pattern:

- **^**: Asserts the start of the string.
- **(?:...)**: A non-capturing group that groups several patterns together.
- Prefixes:
  - **5[1-5][0-9]{2}**: Matches numbers starting with 51 through 55, followed by any two digits.
  - **222[1-9]**: Matches numbers starting with 2221 through 2229.
  - **22[3-9][0-9]**: Matches numbers starting with 223 to 229, followed by any digit (from 0 to 9).
  - **2[3-6][0-9]{2}**: Matches numbers starting with 23 to 26, followed by any two digits.
  - **27[01][0-9]**: Matches numbers starting with 270 or 271, followed by any digit (from 0 to 9).
  - **2720**: Matches numbers starting with 2720.
- **[0-9]{12}**: After matching one of the above prefixes, matches exactly 12 digits, making the total length 16 digits.
- **$**: Asserts the end of the string.

### Details:

- **Card numbers**:
	- **Prefixes**: MasterCards start with ranges between **51 - 55** or **2221 - 2720**	  	
	- **16 digits**: MasterCard numbers are 16 digits long.
- **CVC**: Uses 3 digits

### Sources:

- https://www.regular-expressions.info/creditcard.html


## Identifying American Express

```regex
^3[47][0-9]{13}$
```

### Explanation of the Regex Pattern:

- **^**: Asserts the start of the string.
- **3**: American Express card numbers start with 3.
- **[47]**: The second digit must be either 4 or 7, meaning the card number must start with either 34 or 37.
- **[0-9]{13}**: Matches exactly 13 digits following the initial two digits, making the total length 15 digits.
- **$**: Asserts the end of the string.

### Details:

- **Card numbers**:
	- **34 or 37**: American Express card numbers always start with 34 or 37.
	- **15 digits**: American Express card numbers are 15 digits long.	 
- **CVC**: Uses 4 digits

### Sources:

- https://www.regular-expressions.info/creditcard.html
