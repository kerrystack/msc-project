# 10 Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "Deleting vertical scaler"
kubectl delete vpa vpa-php-apache