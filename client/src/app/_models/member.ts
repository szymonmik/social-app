import { Photo } from "./photo"

export interface Member {
    id: number
    login: string
    firstName: string
    surname: string
    photoUrl: string
    age: number
    created: string
    lastActive: string
    gender: string
    introduction: string
    interests: string
    city: string
    country: string
    photos: Photo[]
  }
  
