Kamailio-ISUP7-Csharp
A Low-Level C# Library for SS7-ISUP Protocol Implementation for SIP-T

This repository contains a low-level library written in C# that implements the SS7-ISUP (Signalling System No. 7 – ISDN User Part) protocol for SIP-T (SIP for Telephones). Inspired by the Kamailio sipt.c module, this library is designed to enable developers to build and integrate SS7-ISUP functionalities into SIP-T environments.
C++ Original code: https://github.com/kamailio/kamailio/blob/master/src/modules/sipt/sipt.c

Overview
The purpose of this library is to provide a robust, flexible, and extensible foundation for:

Encoding and decoding ISUP messages according to ITU-T Recommendation Q.763 and ANSI T1.113.
Integrating with SIP-T systems to support telephony applications that require low-level signaling.
Serving as a reference implementation for projects that need to interact with legacy SS7 networks.

Features
Low-Level ISUP Message Handling:
Implements message format, parameter encoding, and decoding per ITU-T Q.763 and ANSI T1.113 standards.

SIP-T Integration:
Designed to integrate with SIP-T systems, allowing for seamless interoperability between SS7 signaling and SIP-based applications.

Modular Architecture:
Cleanly separated modules for message parsing, encoding, and transport that can be extended or adapted for specific use cases.

Reference Documentation:
This library is built using guidelines from ITU-T Q.763 (12/1999) and ANSI T1.113 documents. For further details, please refer to these standards.

Getting Started

Prerequisites
.NET 6.0 (or later)

Visual Studio 2022 or your preferred C# development environment

Installation
Clone the Repository:

git clone https://github.com/ranvli/kamailio-ISUP7-Csharp.git
cd kamailio-ISUP7-Csharp
Build the Solution: Open the solution in Visual Studio and build it, or use the .NET CLI:

dotnet build
This library is intended to be integrated into larger SIP-T or telecommunication projects. Below is a basic example of how to use the library for encoding an ISUP message:


    // Create a new ISUP message instance
    var isupMessage = new ISUPMessage(MessageType.IAM);
    
    // Set mandatory fields, e.g., Circuit Identification Code (CIC)
    isupMessage.SetCIC(1234);
    
    // Add other parameters as needed
    isupMessage.AddParameter(ParameterType.CallingPartyNumber, "5551234");
    
    // Encode the message into a byte array
    byte[] encodedMessage = isupMessage.Encode();
    
    // Use the encoded message as needed (send over network, etc.)

For a detailed guide on message structure and additional usage examples, please refer to the Documentation.

Contributing
Contributions, issues, and feature requests are welcome. Feel free to check the issues page if you want to contribute.

License
This project is licensed under the MIT License – see the LICENSE file for details.

References
ITU-T Recommendation Q.763 (12/1999)
ANSI T1.113-199x

Feel free to reach out for any questions or further discussion on extending this library.
