
export abstract class ContentBaseModel {
    title: string = '';
    contentHtml: string = '';
}

export class ContactModel extends ContentBaseModel {
    phoneNumber: string = '';
    faxNumber: string = '';
    emailAddress: string = '';
    fullName: string = '';
    streetAndHouseNumber: string = '';
    zipCode: string = '';
    city: string = '';
}

export class HomeModel extends ContentBaseModel {
    pageTitle: string = '';
    welcomeParagraph: string = '';
    mapsLink: string = '';
    roomOneLink: string = '';
    roomTwoLink: string = '';
}

export class LegalModel extends ContentBaseModel {
    contact: string = '';
}

export class LiabilityModel extends ContentBaseModel {
    content: string = '';
    links: string = '';
    copyright: string = '';
    dataProtection: string = '';
}

export class RoomModel extends ContentBaseModel {
    furnishing: string = '';
    pricing: string = '';
    miscellaneous: string = '';
}

