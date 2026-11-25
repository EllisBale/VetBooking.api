export interface VetResponse {
    id: number;
    name: string;
    specialisation: string;
    email: string;
    phoneNumber: string;
}


export interface OwnerResponse {
    id: number;
    name: string;
    email: string;
    address: string;
    phoneNumber: string;
    isActive: boolean;
    petCount: number;
}

export interface PetResponse {
    id: number;
    name: string;
    species: string;
    breed: string;
    dateOfBirth: string;
    isDeceased: boolean;
    deceasedDate: string;
    owner: OwnerResponse;
}


export interface AppointmentSummaryResponse {
    id: number;
    startUtc: string;
    endUtc: string;
    status: string;
    appointmentTypeName: string;
    petName: string;
    ownerName: string;
    vetName: string;
    examRoom: string | number;
}