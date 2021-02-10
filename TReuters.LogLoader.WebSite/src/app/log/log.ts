import { UserAgent } from "./userAgent";

export interface Log {
    logId: number;
    ip: string;
    userIdentifier: string;
    requestDate: Date;
    timezone:string;
    method:string;
    requestURL:string;
    protocol:string;
    protocolVersion:string;
    statusCodeResponse:number;
    port:number
    originUrl:string;
    userAgents:UserAgent[];

	// 1:N - userAgent
	// 	product
	// 	productVersion
	// 	comment
}

