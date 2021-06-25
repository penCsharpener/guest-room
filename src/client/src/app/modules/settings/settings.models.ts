
export abstract class ContentBaseModel {
    title = '';
    contentHtml = '';
}

export class ContactModel extends ContentBaseModel {
    phoneNumber = '';
    faxNumber = '';
    emailAddress = '';
    fullName = '';
    streetAndHouseNumber = '';
    zipCode = '';
    city = '';
}

export class HomeModel extends ContentBaseModel {
    pageTitle = '';
    welcomeParagraph = '';
    mapsLink = '';
    roomOneLink = '';
    roomTwoLink = '';
}

export class LegalModel extends ContentBaseModel {
    legalParagraphs = [] as LegalParagraphModel[];
}

export class LegalParagraphModel {
    heading = '';
    text = '';
}

export class LiabilityModel extends ContentBaseModel {
    content = '';
    links = '';
    copyright = '';
    dataProtection = '';
}

export class RoomModel extends ContentBaseModel {
    furnishing = '';
    pricing = '';
    miscellaneous = '';
}

export class ImageModel {
    file?: File;
    path = '';
    description = '';
    name = '';
    location = '';
}