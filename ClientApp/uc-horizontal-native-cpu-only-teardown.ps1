write-host "Deleting horizontal scaler"
kubectl delete hpa hpa-php-apache

write-host "Deleting deployment"
kubectl delete deployment php-apache