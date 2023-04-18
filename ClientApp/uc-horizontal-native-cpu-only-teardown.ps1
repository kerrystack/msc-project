# 10 Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "Deleting horizontal scaler"
kubectl delete hpa hpa-php-apache