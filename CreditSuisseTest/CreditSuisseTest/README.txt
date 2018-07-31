Task: Implement the following in C#.

A simple vending machine, which has an inventory of 25 cans. 
There are two cash cards linked to one joint account and PIN protected.
When a user buys a soft drink from the machine 50p is taken from a cash card.

Requirements:
1.	Can’t vend more than 25 cans
2.	Can’t vend can if less than 50p on the card or PIN supplied is invalid
3.	Cash card balance should be updated when a can is bought
4.	As there are multiple cash cards linked to one account, the account may be accessed by multiple requests simultaneously

Your solution will be assessed on the following:
- Implementation of the requirements
- Code quality
- Testing style and quality

-Due to time limitation no automated tests were included, they will be added at a later date
-No password encription was implemented due to time limitations

Design Decisions:
-If the pricing policies change,e.g price dependent on drink sold, then the price mapping is the only item that would have to be modified
-The card vending machine inherets from the vending machine as minimal changes would occur if at a later date a different type of vending machine would be used e.g cash only, bluetooth new derived vending machines could be created
-Wanted to ensure classes were losely coupled, the application could be extended at a later date with minimal impact

Assumptions:
I assume the vending machine will only serve one customer at a time.
I assume the vending machines payment terminal will use online authorization and cryptogram verificatiom
I assume the cash cards have EMV chips
I assume the terminal/vending machine will be able to display its state to the user e.g payment accepted
I assume the cashcard paymennt processing will take less then 3 seconds
I assume the transaction can be aborted if the card is removed while this process is taken place
I asume the terminal is configured to also veryfiy the user using the EMV online pin.
I am assuming the user can order more then one drink in one transaction
I assume the smallest interval betweeen users purchasing items is 4 seconds
I assume the vending machine will only stock drinks


EMV Authentication will not be implemented
Processor, Aquiring Bank and Merchants bank will not be included in the cash card payment processing scenario

Sources of information describing the card transaction process and how terminals work
https://www.youtube.com/watch?v=LBiHL_de4YA
https://www.youtube.com/watch?v=nRzTaWZ6ebs
https://www.youtube.com/watch?v=h8TxLRN-SuM
https://www.youtube.com/watch?v=DChVE1NEME0
https://security.stackexchange.com/questions/104076/how-does-a-debit-credit-card-reader-verify-the-pin-so-quickly
https://www.quora.com/Do-chip-and-PIN-debit-cards-store-your-PIN-on-the-card
https://go.eway.io/s/article/Transaction-Response-Codes