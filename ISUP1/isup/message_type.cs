
 
/* ISUPSS7 message types. Q.763 table references in parenthesis. */
public enum isupss7_message_type
{
  ISUPSS7_IAM = 0x01, // Initial address (32)
  ISUPSS7_SAM = 0x02, // Subsequent address (35)
  ISUPSS7_INR = 0x03, // Information request (31)
  ISUPSS7_COT = 0x05, // Continuity (28)
  ISUPSS7_ACM = 0x06, // Address complete (21)
  ISUPSS7_CON = 0x07, // Connect (27)
  ISUPSS7_ANM = 0x09, // Answer (22)
  ISUPSS7_REL = 0x0c, // Release (33)
  ISUPSS7_SUS = 0x0d, // Suspend (38)
  ISUPSS7_RES = 0x0e, // Resume (38)
  ISUPSS7_RLC = 0x10, // Release complete (34)
  ISUPSS7_CCR = 0x11, // Continuity Check Request (39)
  ISUPSS7_RSC = 0x12, // Reset circuit (39)
  ISUPSS7_BLK = 0x13, // Blocking (39)
  ISUPSS7_UBL = 0x14, // Unblocking (39)
  ISUPSS7_BLA = 0x15, // Blocking acknowledgement (39)
  ISUPSS7_UBA = 0x16, // Unblocking acknowledgement (39)
  ISUPSS7_GRS = 0x17, // Circuit group reset (41)
  ISUPSS7_CGB = 0x18, // Curciut group blocking (40)
  ISUPSS7_CGU = 0x19, // Curciut group unblocking (40)
  ISUPSS7_CGA = 0x1a, // Curciut group blocking acknowledgement (40)
  ISUPSS7_CUA = 0x1b, // Curciut group unblocking acknowledgement (40)
  ISUPSS7_GRA = 0x29, // Circuit group reset acknowledgement (25)
  ISUPSS7_CPR = 0x2c, // Call progress (23)
  ISUPSS7_UEC = 0x2e // Unequipped CIC (39)
}

