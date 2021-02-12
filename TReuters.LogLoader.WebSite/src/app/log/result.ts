import { UserAgent } from "./userAgent";

export interface Result<T> {
    success: boolean;
    error: string;
    value: T;    
}

