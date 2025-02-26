export interface JWTClaimsModel {
   aud : string,
   iss : string,
   exp : number,
   jti : string,
   roles : [],
   uid : string,
   username:string
}
