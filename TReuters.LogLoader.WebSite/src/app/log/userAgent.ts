export interface UserAgent {
    id: number;
    product:string;
    productVersion:string;
    systemInformation:string;

	// 1:N - userAgent
	// 	product
	// 	productVersion
	// 	comment
}

